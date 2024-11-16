using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.EnvironmentStandards.Entities;
using SharedKernel;

namespace Accreditation.Domain.AccreditationInstancesEnvironmentStandardsResults.Entities;
public sealed class AccreditationInstancesEnvironmentStandardsResult : Entity
{
    public Guid AccreditationInstanceGUID { get; private set; }
    public Guid EnvironmentStandardGUID { get; private set; }
    public decimal? LevelOneScore { get; private set; }
    public int? LevelOnePossibleScore { get; private set; }
    public decimal? LevelTwoScore { get; private set; }
    public int? LevelTwoPossibleScore { get; private set; }
    public decimal? LevelThreeScore { get; private set; }
    public int? LevelThreePossibleScore { get; private set; }
    public EnvironmentStandard EnvironmentStandard { get; private set; } = null!;
    public AccreditationInstance AccreditationInstance { get; private set; } = null!;
    private AccreditationInstancesEnvironmentStandardsResult(Guid id) => GUID = id;
    private AccreditationInstancesEnvironmentStandardsResult()
    {

    }
    private AccreditationInstancesEnvironmentStandardsResult(Guid accreditationInstanceGUID,
                                                             Guid environmentStandardGUID)
                                                           : base(Guid.NewGuid())
    {
        AccreditationInstanceGUID = accreditationInstanceGUID;
        EnvironmentStandardGUID = environmentStandardGUID;
    }
    public static AccreditationInstancesEnvironmentStandardsResult Create(Guid accreditationInstanceGUID,
                                                                          Guid environmentStandardGUID) =>
        new AccreditationInstancesEnvironmentStandardsResult(accreditationInstanceGUID,
                                                             environmentStandardGUID);

}