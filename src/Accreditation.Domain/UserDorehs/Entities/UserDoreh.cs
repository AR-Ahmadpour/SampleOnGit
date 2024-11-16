using Accreditation.Domain.DorehAmoozeshis.Entities;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.UserDorehs.Entities
{
    public sealed class UserDoreh : Entity
    {

        public Guid UserGuid { get; set; }
        public Guid DorehAmoozeshiGuid { get; set; }
        public string DorehTitle { get; set; }
        public string BargozarKonandeh { get; set; }
        public int DorehHours { get; set; }
        public bool DorehRole { get; set; }
        public DorehAmoozeshi DorehAmoozeshi { get; set; }
        public User User { get; set; }

        public UserDoreh() { }


        private UserDoreh(Guid userGuid, Guid dorehAmoozeshiGuid, string dorehTitle, string bargozarKonandeh,
            int dorehHours, bool dorehRole) : base(Guid.NewGuid())
        {
            UserGuid = userGuid;
            DorehTitle = dorehTitle;
            DorehAmoozeshiGuid = dorehAmoozeshiGuid;
            BargozarKonandeh = bargozarKonandeh;
            DorehHours = dorehHours;
            DorehRole = dorehRole;
        }

        public static UserDoreh Create(Guid userGuid, Guid dorehAmoozeshiGuid, string dorehTitle, string bargozarKonandeh,
            int dorehHours, bool dorehRole)
        {
            return new UserDoreh(userGuid, dorehAmoozeshiGuid, dorehTitle, bargozarKonandeh,
             dorehHours, dorehRole);
        }

        public void Edit(Guid dorehAmoozeshi, string dorehTitle, string bargozarkonandeh, int dorehHours, bool dorehRole)
        {
            DorehAmoozeshiGuid = dorehAmoozeshi;
            DorehTitle = dorehTitle;
            BargozarKonandeh = bargozarkonandeh;
            DorehHours = dorehHours;
            DorehRole = dorehRole;
        }

    }
}
