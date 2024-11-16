using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Fields.GetFilterdList;

public sealed record GetAllFilterdFieldQuery(Guid EtebardorehGuid,
                                      Guid AccreditationalInstaneGuid) :
                                    IQuery<List<GetAllFilteredFieldQueryDto>>;



