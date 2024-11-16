using SharedKernel;

namespace Accreditation.Domain.DorehAmoozeshis.Entities
{
    public sealed class DorehAmoozeshi : Entity
    {
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
    }
}
