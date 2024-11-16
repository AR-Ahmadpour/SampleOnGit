using System;
using System.Collections.Generic;
using Accreditation.Domain.EnvironmentStandards.Entities;

namespace Accreditation.Api.Endpoints.SanjehEnvironmentStandards.Adds
{
    public sealed record AddSanjehEnvironmentStandardRequest(
        Guid SanjehGuid,
        List<Guid> EnvironmentStandardsGuids
    );
}
