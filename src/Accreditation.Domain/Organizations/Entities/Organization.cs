using Accreditation.Domain.CountryDivisions.BakhshLocation.Entities;
using Accreditation.Domain.CountryDivisions.Ostans.Entities;
using Accreditation.Domain.CountryDivisions.Shahr.Entities;
using Accreditation.Domain.CountryDivisions.Shahrestan.Entities;
using Accreditation.Domain.OrgTypes;
using Accreditation.Domain.Universites.Entities;
using SharedKernel;

namespace Accreditation.Domain.Organizations.Entities;

public sealed class Organization : Entity
{
    public string Name { get; private set; }
    public string EnglishName { get; private set; }
    public DateTime UpdateTimestamp { get; private set; }
    public char? RegistrationCode { get; private set; }
    public bool? RegistrationCodeUseFlag { get; private set; }
    public int ExternalId { get; private set; }
    public int BakhshLocationId { get; private set; }
    public int ShahrId { get; private set; }
    public int OstanId { get; private set; }
    public int ShahrestanId { get; private set; }
    public int OwnershipTypeId { get; private set; }
    public int OwnershipSubTypeId { get; private set; } // TODO 
    public int BedCount { get; private set; }
    public bool IsTatilDaem { get; private set; }
    public Guid OrgGerayeshGUID { get; private set; }
    public Guid ExternalGUID { get; private set; }
    public Int64? IntCodeParvaneh { get; private set; }
    public int UniversityId { get; private set; }
    public Guid? OrgTypeGUID { get; private set; }
    public Shahr Shahr { get; private set; } = null!;
    public Ostan Ostan { get; private set; } = null!;
    public Shahrestan Shahrestan { get; private set; } = null!;
    public Accreditation.Domain.OrgGerayeshes.Entities.OrgGerayesh OrgGerayesh { get; private set; } = null!;
    public Bakhsh BakhshLocation { get; private set; } = null!;
    public University University { get; private set; } = null!;
    private Organization() { }

}
