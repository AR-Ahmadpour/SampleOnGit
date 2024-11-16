using Accreditation.Domain.Mehvars.Entities;

namespace Accreditation.Domain.UnitTests.ZirMehvars;

internal static class ZirMehvarData
{
    private static readonly DateTime currentDateTime = DateTime.Now;

    public static readonly Guid MehvarGUID = Guid.NewGuid();
    public static readonly string Title = "LastName";
    public static readonly int WeightedCoefficient = 963;
    public static readonly DateTime CreationDate = currentDateTime;
    public static readonly DateTime? UpdateDate = currentDateTime.AddDays(2);
    public static readonly Guid CreatedByGUID = Guid.NewGuid();
    public static readonly Guid? UpdatedByGUID = Guid.NewGuid();
    public static readonly bool IsDeleted = false;
    public static readonly int SortOrder = 136;
}

