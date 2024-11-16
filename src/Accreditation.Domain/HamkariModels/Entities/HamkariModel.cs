namespace Accreditation.Domain.HamkariModels.Entities
{
    public sealed class HamkariModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public HamkariModel()
        {
            
        }
    }
}
