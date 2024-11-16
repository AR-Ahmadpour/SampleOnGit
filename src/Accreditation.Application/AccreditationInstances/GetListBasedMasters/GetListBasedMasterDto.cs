namespace Accreditation.Application.AccreditationInstances.GetListBasedMasters;

public class GetListBasedMasterDto
{
    public Guid Guid { get; set; }
    public DateOnly? DateFrom{ get; set; }
    public DateOnly? DateTo{ get; set; }
    public int InstanceType{ get; set; }
}
