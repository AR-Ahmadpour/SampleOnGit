using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Fields.GetList;

public sealed record GetAllFieldQuery(Guid EtebardorehGuid,
   Guid AccreditationalInstaneGuid):
 IQuery<List<GetAllFieldQueryDto>>;



