using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.Fields;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs.Add;

public sealed class AddArzyabComomandHandler : ICommandHandler<AddArzyabComomand, Guid>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IEvaluationArzyabFieldRepository _evaluationArzyabFieldRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IAccreditationInstanceRepository _accreditationalInstanceRepository;

    public AddArzyabComomandHandler(IEtebarDorehRepository etebarDorehRepository,
                                    ICurrentUser userContext,
                                    IDateTimeProvider dateTimeProvider,
                                    IUnitOfWork unitOfWork,
                                    IEvaluationArzyabRepository evaluationArzyabRepository,
                                    IEvaluationArzyabFieldRepository evaluationArzyabFieldRepository,
                                    IFieldRepository fieldRepository,
                                    IAccreditationInstanceRepository accreditationalInstanceRepository)
    {
        _etebarDorehRepository = etebarDorehRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _evaluationArzyabFieldRepository = evaluationArzyabFieldRepository;
        _fieldRepository = fieldRepository;
        _accreditationalInstanceRepository = accreditationalInstanceRepository;
    }
    public async Task<Result<Guid>> Handle(
        AddArzyabComomand command,
        CancellationToken cancellationToken)
    {
        if (await _etebarDorehRepository.FindAsync(command.EtebarDorehGuid, cancellationToken) == null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }

        var accInstance = await _accreditationalInstanceRepository.GetByIdAsync(command.AccreditationInstanceGuid, cancellationToken);
        var fields = await _fieldRepository.GetAllByEtebarDorehGuidAsync(command.EtebarDorehGuid, accInstance.InstanceTypeId);
        if (fields.Count != 0 &&
            command.FieldIds != null &&
            command.FieldIds.Except(fields.Select(x => x.Guid)).Any())
        {
            return Result.Failure<Guid>(FieldsErrors.NotFound);
        }

        if (command.AccreditationInstanceGuid != null && accInstance == null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.NotFound);
        }
        if (accInstance.IsLocked == true)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.ArzyabiIsLocked);
        }

        var evaluationArzyabs = await _evaluationArzyabRepository.GetAll(command.AccreditationInstanceGuid, cancellationToken);

        if (!evaluationArzyabs.Any(x => x.RoleId == 1 && x.ArzyabGuid == command.ArzyabUserGuid) && command.ArzyabRoleId == 1)
        {
            return Result.Failure<Guid>(EvaluationArzyabErrors.AlreadySarparastExists);
        }

        var evaluationArzyab = EvaluationArzyab.Create(Guid.Parse(_userContext.UserId),
                                                       command.AccreditationInstanceGuid,
                                                       command.ArzyabUserGuid,
                                                       command.ArzyabRoleId,
                                                       _dateTimeProvider.Now);


        command.FieldIds.ForEach(fieldId => _evaluationArzyabFieldRepository.Add(EvaluationArzyabField.Create(evaluationArzyab.GUID, fieldId)));
        _evaluationArzyabRepository.Add(evaluationArzyab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return evaluationArzyab.GUID;
    }
}