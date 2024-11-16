using Accreditation.Domain.MaghtaTahsilis.Entities;
using Accreditation.Domain.ReshteTahsilis.Entities;
using SharedKernel;

namespace Accreditation.Domain.ReshteMaghtas.Entities
{
    public sealed class ReshteMaghta : Entity
    {
        public Guid ReshtehTahsiliGUID { get; private set; }
        public Guid MaghtaTahsiliGUID { get; private set; }

        public ReshtehTahsili ReshteTahsili { get; private set; } = null!;
        public MaghtaTahsili MaghtaTahsili { get; private set; } = null!;
    }
}
