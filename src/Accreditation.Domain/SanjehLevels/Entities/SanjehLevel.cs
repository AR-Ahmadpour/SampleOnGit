

using Accreditation.Domain.Sanjehs.Entities;
using SharedKernel;

namespace Accreditation.Domain.SanjehLevels.Entities;

public sealed class SanjehLevel:Entity
{
    public string Title { get; private set; }
    public int Id { get; private set; }
    public bool IsDeleted { get; private set; }
    //public ICollection<Sanjeh> Sanjehs { get; set; } = null!;

    public SanjehLevel()
    {
       // Sanjehs = new List<Sanjeh>();
    }
}
