using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.EvaluationArzyabs.Dtos;

namespace Accreditation.Application.EvaluationArzyabs.GetList;

public sealed record GetAllEvaluationArzyabQuery(Guid EtebardorehGuid,
                                                 Guid AccreditationalInstaneGuid) :
                                    IQuery<List<GetAllEvaluationArzyabsDto>>;



