using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceStandardEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceStandards;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceZirMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstancesEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatuses;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.Delete;

internal sealed class DeleteAccInstanceCommandHandler
: ICommandHandler<DeleteAccInstanceCommand>
{
    private readonly IAccInstanceZirMehvarRepository _accInstanceZirMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IAccInstanceStandardRepository _accInstanceStandardRepository;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IAccInstanceZirMehvarEnvironmentStandardsResultRepository _accInstanceZirMehvarEnvironmentStandardsResultRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;
    private readonly IEvaluationArzyabFieldRepository _evaluationArzyabFieldRepository;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IAccreditationInstanceStatusRepository _accreditationInstanceStatusRepository;
    private readonly IAccreditationInstancesEnvironmentStandardsResultRepository _accreditationInstancesEnvironmentStandardsResultRepository;
    private readonly IAccInstanceStandardEnvironmentStandardsResultRepository _accInstanceStandardEnvironmentStandardsResultRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccreditationInstanceAnswerRepository _accreditationInstanceAnswerRepository;

    public DeleteAccInstanceCommandHandler(IAccInstanceZirMehvarRepository accInstanceZirMehvarRepository,
                                           IAccreditationInstanceRepository accreditationInstanceRepository,
                                           IAccInstanceStandardRepository accInstanceStandardRepository,
                                           IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                           IAccInstanceZirMehvarEnvironmentStandardsResultRepository accInstanceZirMehvarEnvironmentStandardsResultRepository,
                                           IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository,
                                           IEvaluationArzyabFieldRepository evaluationArzyabFieldRepository,
                                           IEvaluationArzyabRepository evaluationArzyabRepository,
                                           IAccreditationInstanceStatusRepository accreditationInstanceStatusRepository,
                                           IAccreditationInstancesEnvironmentStandardsResultRepository accreditationInstancesEnvironmentStandardsResultRepository,
                                           IAccInstanceStandardEnvironmentStandardsResultRepository accInstanceStandardEnvironmentStandardsResultRepository,
                                           IEnvironmentStandardRepository environmentStandardRepository,
                                           IUnitOfWork unitOfWork,
                                           IAccreditationInstanceAnswerRepository accreditationInstanceAnswerRepository)
    {
        _accInstanceZirMehvarRepository = accInstanceZirMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _accInstanceStandardRepository = accInstanceStandardRepository;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _accInstanceZirMehvarEnvironmentStandardsResultRepository = accInstanceZirMehvarEnvironmentStandardsResultRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
        _evaluationArzyabFieldRepository = evaluationArzyabFieldRepository;
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _accreditationInstanceStatusRepository = accreditationInstanceStatusRepository;
        _accreditationInstancesEnvironmentStandardsResultRepository = accreditationInstancesEnvironmentStandardsResultRepository;
        _accInstanceStandardEnvironmentStandardsResultRepository = accInstanceStandardEnvironmentStandardsResultRepository;
        _environmentStandardRepository = environmentStandardRepository;
        _unitOfWork = unitOfWork;
        _accreditationInstanceAnswerRepository = accreditationInstanceAnswerRepository;
    }
    public async Task<Result> Handle(DeleteAccInstanceCommand request, CancellationToken cancellationToken)
    {
        var accreditationInstance = await _accreditationInstanceRepository.FindAsync(request.GUID);
        if (accreditationInstance == null)
        {
            return Result.Failure(AccreditationInstanceErrors.NotFound);
        }
        if (accreditationInstance.IsLocked)
        {
            return Result.Failure(AccreditationInstanceErrors.IsLockedDelete);
        }
        
        var isAccInstancePyeh = await _accreditationInstanceRepository.FindIsPayehAsync(accreditationInstance.GUID);
        if (isAccInstancePyeh)
        {
            return Result.Failure(AccreditationInstanceErrors.IsLockedMasterDelete);
        }
        var evaluationArzyabs = await _evaluationArzyabRepository.GetAllBasedAccins(accreditationInstance.GUID, cancellationToken);
        if (evaluationArzyabs != null && evaluationArzyabs.Count != 0)
        {
            evaluationArzyabs.ForEach(evaluationArzyab => _evaluationArzyabRepository.Delete(evaluationArzyab));
        }

        var accreditationInstanceStatus = await _accreditationInstanceStatusRepository.FindBasedAccInstanceAsyc(accreditationInstance.GUID, cancellationToken);
        _accreditationInstanceStatusRepository.Delete(accreditationInstanceStatus);

        var accInstanceEnvResults = await _accreditationInstancesEnvironmentStandardsResultRepository.GetListAccInstanceAsync(accreditationInstance.GUID);
        accInstanceEnvResults.ForEach(accInstanceEnvResul => _accreditationInstancesEnvironmentStandardsResultRepository.Delete(accInstanceEnvResul));

        var accreditationInstanceAnswers = await _accreditationInstanceAnswerRepository.GetListAccInstanceAnswersAsync(accreditationInstance.GUID);
        accreditationInstanceAnswers.ForEach(accreditationInstanceAnswer => _accreditationInstanceAnswerRepository.Delete(accreditationInstanceAnswer));

        var accInstanceStandards = await _accInstanceStandardRepository.FindByAccInstanceGuid(accreditationInstance.GUID);
        if (accInstanceStandards != null)
        {
            foreach (var accInstanceStandard in accInstanceStandards)
            {
                var accInstanceStandardResults = await _accInstanceStandardEnvironmentStandardsResultRepository.GetListByAccInstanceStandardAsync(accInstanceStandard.GUID);
                if (accInstanceStandardResults != null)
                {
                    foreach (var accInstanceStandardResult in accInstanceStandardResults)
                    {
                        _accInstanceStandardEnvironmentStandardsResultRepository.Delete(accInstanceStandardResult);
                    }
                }
                _accInstanceStandardRepository.Delete(accInstanceStandard);
            }
        }

        var accInstanceZirMehvarz = await _accInstanceZirMehvarRepository.GetListAccInstanceZirMehvarAsync(accreditationInstance.GUID);
        if (accInstanceZirMehvarz != null)
        {
            foreach (var accInstanceZirMehvar in accInstanceZirMehvarz)
            {
                var accInstanceZirMehvarResults = await _accInstanceZirMehvarEnvironmentStandardsResultRepository.GetListAccInstanceZirMehvarAsync(accInstanceZirMehvar.GUID);
                if (accInstanceZirMehvarResults != null)
                {
                    foreach (var accInstanceMehvarResult in accInstanceZirMehvarResults)
                    {
                        _accInstanceZirMehvarEnvironmentStandardsResultRepository.Delete(accInstanceMehvarResult);
                    }
                }
                _accInstanceZirMehvarRepository.Delete(accInstanceZirMehvar);
            }
        }

        var accInstanceMehvarz = await _accInstanceMehvarRepository.GetListAccInstanceMehvarAsync(request.GUID);
        if (accInstanceMehvarz != null)
        {
            foreach (var accInstanceMehvar in accInstanceMehvarz)
            {
                var accInstanceMehvarResults = await _accInstanceMehvarEnvironmentStandardsResultRepository.GetListAccInstanceMehvarAsync(accInstanceMehvar.GUID);
                if (accInstanceMehvarResults != null)
                {
                    foreach (var accInstanceMehvarResult in accInstanceMehvarResults)
                    {
                        _accInstanceMehvarEnvironmentStandardsResultRepository.Delete(accInstanceMehvarResult);
                    }
                }
                _accInstanceMehvarRepository.Delete(accInstanceMehvar);
            }
        }

        if (accreditationInstance != null)
        {
            _accreditationInstanceRepository.Delete(accreditationInstance);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
