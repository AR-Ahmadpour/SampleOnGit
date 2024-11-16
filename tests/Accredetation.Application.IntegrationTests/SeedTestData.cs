using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.OrgTypes;
using Accreditation.Infrastructure.Database;

namespace Accreditation.Application.IntegrationTests;
public static class SeedTestData
{
    private static readonly object _lock = new object();
    private static bool _initialized = false;
    private static Guid _orgGuid;
    private static Guid _etebarDorehGuid;

    public static void Initialize(AccreditationDbContext context)
    {
        if (_initialized) return;

        lock (_lock)
        {
            if (_initialized) return;

            _orgGuid = DataOrganization(context);
            _etebarDorehGuid = DataEtebarDoreh(context, _orgGuid);

            _initialized = true;
        }
    }

    public static Guid DataOrganization(AccreditationDbContext context)
    {
        if (context.OrgType.Any())
        {
            return context.OrgType.FirstOrDefault(x => x.Title == "موسسه").GUID;
        }
        var org = OrgType.Create("1", "3", "موسسه", false, 1);
        context.OrgType.Add(org);
        context.SaveChanges();
        return org.GUID;
    }

    public static Guid DataEtebarDoreh(AccreditationDbContext context, Guid orgGuidId)
    {
        if (context.EtebarDorehs.Any())
        {
            return context.EtebarDorehs.FirstOrDefault(x => x.Title == "اعتبار دوره تست").GUID;
        }
        var etebarDoreh = EtebarDoreh.Create(orgGuidId, "اعتبار دوره تست", DateTime.Now, Guid.NewGuid(), new DateOnly(2023, 6, 1), new DateOnly(2023, 7, 1), true, 1);
        context.EtebarDorehs.Add(etebarDoreh);
        context.SaveChanges();
        return etebarDoreh.GUID;
    }

    public static Guid OrgGuid => _orgGuid;
    public static Guid EtebarDorehGuid => _etebarDorehGuid;
}


