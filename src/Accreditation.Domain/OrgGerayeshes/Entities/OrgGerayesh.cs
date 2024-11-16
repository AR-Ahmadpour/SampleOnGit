using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.OrgTypes;
using SharedKernel;

namespace Accreditation.Domain.OrgGerayeshes.Entities;

public sealed class OrgGerayesh : Entity
{
    public Int64? IntCodeParvaneh { get; private set; }
    public Int64? IntCodePaygah { get; private set; }
    public string Title { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid OrgTypeGUID { get; private set; }
    public OrgType OrgType { get; private set; } = null!;
    //public ICollection<NotNaSanjeh> NotNaSanjehs { get; private set; }
  
    public OrgGerayesh()
    {
        //NotNaSanjehs = new List<NotNaSanjeh>();  
    }

    public OrgGerayesh(Guid gUID, Int64? intCodeParvaneh,
                       Int64? intCodePaygah, string title, Guid orgTypeGuid)
    : base(gUID)
    {
        Title = title;
        IntCodeParvaneh = IntCodeParvaneh;
        IntCodePaygah = intCodeParvaneh;
        OrgTypeGUID = orgTypeGuid;
        IsDeleted = false;
    }
    public static OrgGerayesh Create (Guid gUID, Int64? intCodeParvaneh,
                                     Int64? intCodePaygah, string title, Guid orgTypeGuid)
   => new OrgGerayesh(Guid.NewGuid(), intCodeParvaneh,
                       intCodePaygah, title, orgTypeGuid);
}

