using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.EvaluationArzyabs.Edit;
using Accreditation.Application.Fields;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs.Add;

public sealed class EditArzyabComomandHandler : ICommandHandler<EditArzyabComomand, Guid>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IEvaluationArzyabFieldRepository _evaluationArzyabFieldRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IAccreditationInstanceRepository _accreditationalInstanceRepository;

    public EditArzyabComomandHandler(IEtebarDorehRepository etebarDorehRepository,
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
    EditArzyabComomand command,
        CancellationToken cancellationToken)
    {
        var evaluationArzyab = await _evaluationArzyabRepository.FindAsync(command.EvaluationArzyabGuid, cancellationToken);
        var accInstance = await _accreditationalInstanceRepository.GetByIdAsync(evaluationArzyab.AccreditationInstanceGUID, cancellationToken);
        var evaluationArzyabs = await _evaluationArzyabRepository.GetAllBasedAccins(accInstance.GUID, cancellationToken);
        var sarparast = evaluationArzyabs.FirstOrDefault(x => x.ArzyabRoleId == 1);


        if (accInstance == null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.NotFound);
        }
      
        if (evaluationArzyab is null)
        {
            return Result.Failure<Guid>(EvaluationArzyabErrors.ArzyabNotFound);
        }

        if (sarparast != null  &&  command.ArzyabRoleId == 1 && sarparast.GUID != command.EvaluationArzyabGuid)
        {
            return Result.Failure<Guid>(EvaluationArzyabErrors.AlreadySarparastExists);
        }
        var fields = await _fieldRepository.GetAllByEtebarDorehGuidAsync(accInstance.EtebarDorehGUID, accInstance.InstanceTypeId);

        if (fields.Count != 0 &&
            command.FieldIds != null && 
            command.FieldIds.Except(fields.Select(x => x.Guid)).Any())
        {
            return Result.Failure<Guid>(FieldsErrors.NotFound);
        }

        if (await _etebarDorehRepository.FindAsync(accInstance.EtebarDorehGUID, cancellationToken) == null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }

        if (sarparast is not null && sarparast.GUID != command.EvaluationArzyabGuid && command.ArzyabRoleId == 1)
        {
            return Result.Failure<Guid>(EvaluationArzyabErrors.SarparastMustExist);
        }
      
        if (accInstance.IsLocked == true)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.ArzyabiIsLocked);
        }

        _evaluationArzyabFieldRepository.Delete(evaluationArzyab);
        command.FieldIds.ForEach(fieldId => _evaluationArzyabFieldRepository.Add(EvaluationArzyabField.Create(evaluationArzyab.GUID, fieldId)));
        
        evaluationArzyab.Edit(command.ArzyabRoleId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return evaluationArzyab.GUID;
    }
}