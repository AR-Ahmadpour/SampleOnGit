using Accreditation.Domain.AccInstanceMehvars.Entities;
using Accreditation.Domain.EnvironmentStandards.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceMehvarEnvironmentStandardsResults.Entities;
public sealed class AccInstanceMehvarEnvironmentStandardsResult : Entity
{
    public Guid AccInstanceMehvarGUID { get; private set; }
    public Guid EnvironmentStandardGUID { get; private set; }
    public decimal? LevelOneScore { get; private set; }
    public int? LevelOnePossibleScore { get; private set; }
    public decimal? LevelTwoScore { get; private set; }
    public int? LevelTwoPossibleScore { get; private set; }
    public decimal? LevelThreeScore { get; private set; }
    public int? LevelThreePossibleScore { get; private set; }
    public EnvironmentStandard EnvironmentStandard { get; private set; } = null!;
    public AccInstanceMehvar AccInstanceMehvar { get; private set; } = null!;
    private AccInstanceMehvarEnvironmentStandardsResult(Guid id) => GUID = id;
    private AccInstanceMehvarEnvironmentStandardsResult()
    {

    }
    private AccInstanceMehvarEnvironmentStandardsResult(Guid accInstanceMehvarGUID,
                                                        Guid environmentStandardGUID)
                                                       : base(Guid.NewGuid())
    {
        AccInstanceMehvarGUID = accInstanceMehvarGUID;
        EnvironmentStandardGUID = environmentStandardGUID;
    }
    public static AccInstanceMehvarEnvironmentStandardsResult Create(Guid accInstanceMehvarGUID,
                                                                     Guid environmentStandardGUID) =>
        new AccInstanceMehvarEnvironmentStandardsResult(accInstanceMehvarGUID,
                                                        environmentStandardGUID);

}