using Accreditation.Domain.CountryDivisions.Ostans.Entities;
using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.Shahr.Entities
{
    public sealed class Shahr /*: Entity*/
    {
        public int Id { get; private set; }
        public string? IntCode { get; private set; }
        public string Title { get; private set; }
        public string EnglishName { get; private set; }//200
        public int? ShahreDaneshgahi { get; private set; }
        public int? ShahreBozorg { get; private set; }
        public int BakhshId { get; private set; }
        public Accreditation.Domain.CountryDivisions.BakhshLocation.Entities.Bakhsh BakhshLocation { get; private set; } = null!;
        public bool IsDeleted { get; private set; }
        public int SortOrder{ get; private set; }
        public Ostan Ostan { get;private set; }
        public int OstanId { get;private set; }
        private Shahr() { }

        //private Shahr(Guid id) => GUID = id;

        public Shahr( string intCode, string title, bool isDeleted, int orderId,
                     string englishTitle, int? shahreDaneshgahi, int? shahreBozorg)
        //: /*base(gUID)*/
        {
            IntCode = intCode;
            Title = title;
            IsDeleted = isDeleted;
            SortOrder = orderId;
            EnglishName = englishTitle;
            ShahreDaneshgahi = shahreDaneshgahi;
            ShahreBozorg = shahreBozorg;

        }
        public static Shahr Create
           (string intCode, string title, bool isDeleted, int orderId, string englishTitle, int shahreDaneshgahi, int? shahreBozorg)
       => new Shahr( intCode, title, isDeleted, orderId, englishTitle, shahreDaneshgahi, shahreBozorg);
    }
}
