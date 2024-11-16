using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Domain.Common.Enums;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.GetById;
internal sealed class GetAccreditationalInstanceByIdQueryHandler
: IQueryHandler<GetAccreditationalInstanceByIdQuery, GetAccreditationalInstanceByIdDto>
{

    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;

    public GetAccreditationalInstanceByIdQueryHandler(IAccreditationInstanceRepository accreditationInstanceRepository,
                                                      IEvaluationArzyabRepository evaluationArzyabRepository)
    {
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _evaluationArzyabRepository = evaluationArzyabRepository;
    }

    public async Task<Result<GetAccreditationalInstanceByIdDto>> Handle(GetAccreditationalInstanceByIdQuery request,
                                                                            CancellationToken cancellationToken)
    {
        var result = await _accreditationInstanceRepository.GetByIdAsync( request.AccreditationInstanceId,cancellationToken = default);

        var payehName = "";
        var arzyabName = "";
        var evaluationArzyabs = await _evaluationArzyabRepository.GetAllBasedAccins(request.AccreditationInstanceId, cancellationToken);
        var ArzyabSarparast = evaluationArzyabs.FirstOrDefault(x => x.ArzyabRoleId == 1);

        if (evaluationArzyabs != null)
        {
            arzyabName = ArzyabSarparast == null ? "" : ArzyabSarparast.User.FirstName + " " + ArzyabSarparast.User.LastName;
        }
        if (result.MasterGUID != null)
        {
            var AccInstancepayeh = await _accreditationInstanceRepository.GetByIdAsync(result.MasterGUID.Value, cancellationToken = default);
            payehName = ((InstanceTypes)AccInstancepayeh.InstanceTypeId).GetDescription() + " از " + PersianDateHelper.ToPersianDateString(AccInstancepayeh.FromDate)  + " تا " + PersianDateHelper.ToPersianDateString(AccInstancepayeh.ToDate);
        }
        return  new GetAccreditationalInstanceByIdDto
        {
            Guid = result.GUID,
            ArzyabiPayehGuid = result.MasterGUID,
            ArzyabiPayehName = payehName,
            FromDate = result.FromDate,
            ToDate = result.ToDate,
            SarparastGuid = ArzyabSarparast?.GUID,
            SarparastName = arzyabName
        };
    }
}
