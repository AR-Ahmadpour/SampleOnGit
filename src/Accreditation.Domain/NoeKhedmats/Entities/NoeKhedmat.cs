namespace Accreditation.Domain.NoeKhedmats.Entities
{
    public sealed class NoeKhedmat
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public NoeKhedmat()
        {
            
        }
    }
}
