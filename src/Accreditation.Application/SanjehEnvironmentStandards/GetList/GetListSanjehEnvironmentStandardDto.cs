

namespace Accreditation.Application.SanjehEnvironmentStandards.GetList;

public sealed record GetListSanjehEnvironmentStandardDto
{
    public Guid Guid { get; set; }
    public Guid SanjehGuid { get; set; }
    public Guid EnvironmentStandardGuid { get; set; }
}
