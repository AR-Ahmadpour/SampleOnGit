using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.AccreditationInstances.GetListBasedMasters;
public sealed record GetListBasedMasterQuery(
    Guid AccInstanceGuid) :
    IQuery<List<GetListBasedMasterDto>>;
