using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.InstanceType.Getlist;

public sealed record InstanceTypeQuery() : IQuery<List<InstanseTypeResultDto>>;
