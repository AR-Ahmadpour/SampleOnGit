using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Standards.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceStandards.Entities;

public sealed class AccInstanceStandard : Entity
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid AccInstanceZirMehvarGUID { get; private set; }
    public Guid StandardGUID { get; private set; }
    public decimal? LevelOneScore { get; private set; }
    public int? LevelOnePossibleScore { get; private set; }
    public decimal? LevelTwoScore { get; private set; }
    public int? LevelTwoPossibleScore { get; private set; }
    public decimal? LevelThreeScore { get; private set; }
    public int? LevelThreePossibleScore { get; private set; }
    public AccreditationInstance AccreditationInstance { get; private set; } = null!;
    public AccInstanceZirMehvar AccInstanceZirMehvar { get; private set; } = null!;
    public Standard Standard { get; private set; } = null!;
    private AccInstanceStandard(Guid id) => GUID = id;
    private AccInstanceStandard()
    {

    }
    private AccInstanceStandard(Guid accreditationInstanceGUID,
                                 Guid accInstanceZirMehvarGUID,
                                 Guid standardGUID)
                                : base(Guid.NewGuid())
    {
        StandardGUID = standardGUID;
        AccInstanceZirMehvarGUID = accInstanceZirMehvarGUID;
        AccreditationInstanceGUID = accreditationInstanceGUID;
    }
    public static AccInstanceStandard Create(Guid accreditationInstanceGUID,
                                              Guid accInstanceZirMehvarGUID,
                                              Guid standardGUID) =>
        new AccInstanceStandard(accreditationInstanceGUID,
                                 accInstanceZirMehvarGUID,
                                 standardGUID);

}