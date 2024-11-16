using Accreditation.Domain.EnvironmentStandards.Entities;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using SharedKernel;

namespace Accreditation.Domain.SanjeEnvironemtnStandards.Entities
{
    public sealed class SanjehEnvironmentStandard : Entity
    {
        public Guid SanjehGUID { get; private set; }
        public Guid EnvironmentStandardGUID { get; private set; }
        public Sanjeh Sanjeh { get; private set; } = null!;
        public EnvironmentStandard EnvironmentStandard { get; private set; } = null!;


        public SanjehEnvironmentStandard()
        {

        }


        private SanjehEnvironmentStandard(Guid sanjehGuid, Guid environmentStandardGuid) : base(Guid.NewGuid())
        {
            SanjehGUID = sanjehGuid;
            EnvironmentStandardGUID = environmentStandardGuid;

        }


        public static SanjehEnvironmentStandard Create(Guid sanjehGuid, Guid environmentStandardGuid)
        {
            return new SanjehEnvironmentStandard(sanjehGuid, environmentStandardGuid);
        }

    }
}
