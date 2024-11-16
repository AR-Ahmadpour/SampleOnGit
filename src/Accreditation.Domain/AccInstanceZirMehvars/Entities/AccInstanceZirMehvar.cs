using Accreditation.Domain.AccInstanceMehvars.Entities;
using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceZirMehvars.Entities;

public sealed class AccInstanceZirMehvar : Entity
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid AccInstanceMehvarGUID { get; private set; }
    public Guid ZirMehvarGUID { get; private set; }
    public decimal LevelOneScore { get; private set; }
    public int LevelOnePossibleScore { get; private set; }
    public decimal LevelTwoAsasiScore { get; private set; }
    public int LevelTwoPossibleScore { get; private set; }
    public decimal LevelThreeIdealScore { get; private set; }
    public int LevelThreePossibleScore { get; private set; }
    public AccreditationInstance AccreditationInstance { get; private set; } = null!;
    public AccInstanceMehvar AccInstanceMehvar { get; private set; } = null!;
    public ZirMehvar ZirMehvar { get; private set; } = null!;
    private AccInstanceZirMehvar(Guid id) => GUID = id;
    private AccInstanceZirMehvar()
    {

    }
    private AccInstanceZirMehvar(Guid accreditationInstanceGUID,
                                 Guid accInstanceMehvarGUID,
                                 Guid zirMehvarGUID)
                                : base(Guid.NewGuid())
    {
        ZirMehvarGUID = zirMehvarGUID;
        AccInstanceMehvarGUID = accInstanceMehvarGUID;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
    public static AccInstanceZirMehvar Create(Guid accreditationInstanceGUID,
                                              Guid accInstanceMehvarGUID,
                                              Guid zirMehvarGUID) =>
        new AccInstanceZirMehvar(accreditationInstanceGUID,
                                 accInstanceMehvarGUID,
                                 zirMehvarGUID);

}
