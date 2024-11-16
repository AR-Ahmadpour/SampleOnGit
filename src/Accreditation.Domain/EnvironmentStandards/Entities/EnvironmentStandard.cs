

using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.SanjeEnvironemtnStandards.Entities;
using SharedKernel;

namespace Accreditation.Domain.EnvironmentStandards.Entities;

public sealed class EnvironmentStandard : Entity
{
    public string Title { get; private set; }
    public int? SpecialWeightedCoefficient { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid EtebarDorehGUID { get; private set; }
    public EtebarDoreh EtebarDoreh { get; private set; } = null!;
    //public ICollection<SanjehEnvironmentStandard> SanjehEnvironmentStandard { get; private set; }   
   
    public EnvironmentStandard()
    {
       //SanjehEnvironmentStandard =  new List<SanjehEnvironmentStandard>();  
    }
}
