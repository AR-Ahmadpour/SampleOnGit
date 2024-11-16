using Accreditation.Domain.EtebarDorehs.Entities;

namespace Accreditation.Domain.UnitTests.Mehvars;

internal static class MehvarData
{
    private static readonly DateTime currentDateTime = DateTime.Now;

    public static readonly Guid EtebarDorehGUID = Guid.NewGuid();
    public static readonly string Title = "MehvarTitle";
    public static readonly bool IsDeleted = false;
    public static readonly int WeightedCoefficient = 963; 
    public static readonly int SortOrder = 136;
    public static readonly DateTime CreationDate = currentDateTime;
    public static readonly DateTime? UpdateDate = currentDateTime.AddDays(2);
    public static readonly Guid CreatedByGUID = Guid.NewGuid();
    public static readonly Guid? UpdatedByGUID = Guid.NewGuid();
}

