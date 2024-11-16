using SharedKernel;

namespace Accreditation.Domain.UnitTests.EtebarDorehs;

internal static class ZirMehvarData
{
    private static readonly DateTime currentDateTime = DateTime.Now;
    private static readonly DateOnly currentDate = currentDateTime.ToDateOnly();

    public static readonly Guid OrgTypeGUID = Guid.NewGuid();
    public static readonly string Title = "DUMMY_Title";
    public static readonly DateTime CreationDate = currentDateTime;
    public static readonly DateTime? UpdateDate = currentDateTime.AddDays(2);
    public static readonly Guid CreatedByGUID = Guid.NewGuid();
    public static readonly Guid? UpdatedByGUID = Guid.NewGuid();
    public static readonly DateOnly StartDate = currentDate;
    public static readonly DateOnly EndDate = currentDate.AddDays(2);
    public static readonly bool IsCurrent = true;
    public static readonly bool IsDeleted = false;
    public static readonly int SortOrder = 136;
}

