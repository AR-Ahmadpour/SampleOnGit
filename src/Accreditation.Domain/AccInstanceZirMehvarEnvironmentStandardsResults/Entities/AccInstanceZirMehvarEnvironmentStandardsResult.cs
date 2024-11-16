using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using Accreditation.Domain.EnvironmentStandards.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceZirMehvarEnvironmentStandardsResults.Entities;
public sealed class AccInstanceZirMehvarEnvironmentStandardsResult : Entity
{
    public Guid AccInstanceZirMehvarGUID { get; private set; }
    public Guid EnvironmentStandardGUID { get; private set; }
    public decimal? LevelOneScore { get; private set; }
    public int? LevelOnePossibleScore { get; private set; }
    public decimal? LevelTwoScore { get; private set; }
    public int? LevelTwoPossibleScore { get; private set; }
    public decimal? LevelThreeScore { get; private set; }
    public int? LevelThreePossibleScore { get; private set; }
    public EnvironmentStandard EnvironmentStandard { get; private set; } = null!;
    public AccInstanceZirMehvar AccInstanceZirMehvar { get; private set; } = null!;
    private AccInstanceZirMehvarEnvironmentStandardsResult(Guid id) => GUID = id;
    private AccInstanceZirMehvarEnvironmentStandardsResult()
    {

    }
    private AccInstanceZirMehvarEnvironmentStandardsResult(Guid accInstanceZirMehvarGUID,
                                                           Guid environmentStandardGUID)
                                                          : base(Guid.NewGuid())
    {
        AccInstanceZirMehvarGUID = accInstanceZirMehvarGUID;
        EnvironmentStandardGUID = environmentStandardGUID;
    }
    public static AccInstanceZirMehvarEnvironmentStandardsResult Create(Guid accInstanceZirMehvarGUID,
                                                                     Guid environmentStandardGUID) =>
        new AccInstanceZirMehvarEnvironmentStandardsResult(accInstanceZirMehvarGUID,
                                                        environmentStandardGUID);

}