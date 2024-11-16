using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Mehvars.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceMehvars.Entities;

public sealed class AccInstanceMehvar : Entity
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid MehvarGUID { get; private set; }
    public decimal LevelOneScore { get; private set; }
    public int LevelOnePossibleScore { get; private set; }
    public decimal LevelTwoScore { get; private set; }
    public int LevelTwoPossibleScore { get; private set; }
    public decimal LevelThreeScore { get; private set; }
    public int LevelThreePossibleScore { get; private set; }
    public AccreditationInstance AccreditationInstance { get; private set; } = null!;
    public Mehvar Mehvar { get; private set; } = null!;
    private AccInstanceMehvar(Guid id) => GUID = id;
    private AccInstanceMehvar()
    {

    }
    private AccInstanceMehvar(Guid accreditationInstanceGUID,
                              Guid mehvarGUID)
                             : base(Guid.NewGuid())
    {
        MehvarGUID = mehvarGUID;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
    public static AccInstanceMehvar Create(Guid accreditationInstanceGUID,
                                           Guid mehvarGUID) =>
        new AccInstanceMehvar(accreditationInstanceGUID,
                              mehvarGUID);

}
