using Accreditation.Domain.AccInstanceStandards.Entities;
using Accreditation.Domain.EnvironmentStandards.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccInstanceStandardEnvironmentStandardsResults.Entities;
public sealed class AccInstanceStandardEnvironmentStandardsResult : Entity
{
    public Guid EnvironmentStandardGUID { get; private set; }
    public Guid AccInstanceStandardGUID { get; private set; }
    public decimal? LevelOneScore { get; private set; }
    public int? LevelOnePossibleScore { get; private set; }
    public decimal? LevelTwoScore { get; private set; }
    public int? LevelTwoPossibleScore { get; private set; }
    public decimal? LevelThreeScore { get; private set; }
    public int? LevelThreePossibleScore { get; private set; }
    public EnvironmentStandard EnvironmentStandard { get; private set; } = null!;
    public AccInstanceStandard AccInstanceStandard { get; private set; } = null!;
    private AccInstanceStandardEnvironmentStandardsResult(Guid id) => GUID = id;
    private AccInstanceStandardEnvironmentStandardsResult()
    {

    }
    private AccInstanceStandardEnvironmentStandardsResult(Guid accInstanceStandardGUID,
                                                           Guid environmentStandardGUID)
                                                          : base(Guid.NewGuid())
    {
        AccInstanceStandardGUID = accInstanceStandardGUID;
        EnvironmentStandardGUID = environmentStandardGUID;
    }
    public static AccInstanceStandardEnvironmentStandardsResult Create(Guid accInstanceStandardGUID,
                                                                     Guid environmentStandardGUID) =>
        new AccInstanceStandardEnvironmentStandardsResult(accInstanceStandardGUID,
                                                        environmentStandardGUID);

}