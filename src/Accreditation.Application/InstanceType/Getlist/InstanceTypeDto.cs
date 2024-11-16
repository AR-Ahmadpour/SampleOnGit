namespace Accreditation.Application.InstanceType.Getlist;

public class InstanceTypeDto
{
    public int Id{ get; set; }
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public bool IsActiveInUniversity { get; set; }
    public bool IsActiveInStaff { get; set; }
}
