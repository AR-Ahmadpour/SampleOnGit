namespace Accreditation.Application.AccreditationInstances.GetById;

public sealed class  GetAccreditationalInstanceByIdDto
{
    public Guid Guid { get; set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public string? ArzyabiPayehName { get; set; }
    public Guid? ArzyabiPayehGuid { get; set; }
    public string? SarparastName { get; set; }
    public Guid? SarparastGuid { get; set; }
}
