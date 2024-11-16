namespace Accreditation.Application.Fields.GetHierarchy
{
    public sealed class HierarchyNodeDto
    {

        public string Title { get; set; }
        public Guid? Guid { get; set; }
        public List<HierarchyNodeDto> Children { get; set; } = new List<HierarchyNodeDto>();
        public int Level { get; set; }

    }
}
