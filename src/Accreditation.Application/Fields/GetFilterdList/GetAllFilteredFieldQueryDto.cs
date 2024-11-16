namespace Accreditation.Application.Fields.GetFilterdList;

public sealed class GetAllFilteredFieldQueryDto
{
    public Guid Guid { get; set; }
    public Guid EtebarDorehGuid { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }
 }
