
using Accreditation.Domain.OrgGerayeshes.Entities;
using Accreditation.Domain.SanjeEnvironemtnStandards.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using SharedKernel;

namespace Accreditation.Domain.NotNaSanjehs.Entities
{
    public sealed class NotNaSanjeh:Entity
    {
        public Guid OrgGerayeshGUID { get; private set; }
        public Guid SanjehGUID { get; private set; }
        public Guid CreatedByGUID { get; private set; }
        public DateTime CreationDate { get; private set; }
        public Sanjeh Sanjeh { get; private set; } = null!;
        public OrgGerayesh OrgGerayesh { get; private set; } = null!;



        public NotNaSanjeh()
        {
            
        }


        private NotNaSanjeh(Guid sanjehGuid, Guid orgGerayeshGuid, Guid createByUserGUID, DateTime creationDate) : base(Guid.NewGuid())
        {
            SanjehGUID = sanjehGuid;
            OrgGerayeshGUID = orgGerayeshGuid;
            CreatedByGUID = createByUserGUID;
            CreationDate = creationDate;

        }


        public static NotNaSanjeh Create(Guid sanjehGuid, Guid orgGerayeshGuid,Guid createByUserGUID,
            DateTime creationDate)
        {
            return new NotNaSanjeh(sanjehGuid, orgGerayeshGuid,createByUserGUID,creationDate);
        }
    }
}
