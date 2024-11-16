using SharedKernel;

namespace Accreditation.Domain.ReshteTahsilis.Entities
{
    public sealed class ReshtehTahsili:Entity
    {
        public string Title { get; private set; }
        public int SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
