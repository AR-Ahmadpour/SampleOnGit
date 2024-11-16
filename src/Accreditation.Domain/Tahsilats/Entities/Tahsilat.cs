using Accreditation.Domain.MaghtaTahsilis.Entities;
using Accreditation.Domain.ReshteTahsilis.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.Tahsilats.Entities
{
    public sealed class Tahsilat : Entity
    {
        public Guid UserGUID { get; set; }
        public Guid ReshtehTahsiliGUID { get; set; }
        public Guid MaghtaTahsiliGUID { get; set; }
        public string? MadrakGUID { get; set; }
        public string UniversityName { get; set; }
        public string GraduationDate { get; set; }


        public User User { get; set; }
        public ReshtehTahsili ReshtehTahsili { get; set; }
        public MaghtaTahsili MaghtaTahsili { get; set; }


        private Tahsilat()
        {

        }


        private Tahsilat(Guid userGuid, Guid reshtehTahsiliGuid, Guid maghtaTahsiliGuid, string madrakGuid,
            string universityName, string graduationDate)
        : base(Guid.NewGuid())
        {
            UserGUID = userGuid;
            ReshtehTahsiliGUID = reshtehTahsiliGuid;
            MaghtaTahsiliGUID = maghtaTahsiliGuid;
            MadrakGUID = madrakGuid;
            UniversityName = universityName;
            GraduationDate = graduationDate;
        }


        public static Tahsilat Create(Guid userGuid, Guid reshtehTahsiliGuid, Guid maghtaTahsiliGuid, string? madrakGuid,
            string universityName, string graduationDate)
        {
            return new Tahsilat(userGuid, reshtehTahsiliGuid, maghtaTahsiliGuid, madrakGuid,
             universityName, graduationDate);
        }
        


        public void Edit(Guid maghtaTahsili , Guid reshtehMaghtah, string universityName, string graduationDate,
            string? madrakGuid)
        {
            MaghtaTahsiliGUID = maghtaTahsili;
            ReshtehTahsiliGUID = reshtehMaghtah;
            UniversityName = universityName;
            GraduationDate = graduationDate;
            MadrakGUID = madrakGuid;
        }
    }
}