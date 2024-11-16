using Accreditation.Domain.EtebarDorehs.Entities;
using SharedKernel;

namespace Accreditation.Application.UnitTests.EtebarDorehs;

internal static class EtebarDorehData
{ 
    public static EtebarDoreh Create()
        => EtebarDoreh.Create(
              Guid.NewGuid(),
              "DUMMY_Title_Create",
              DateTime.Now,
              Guid.NewGuid(),
              DateTime.Now.ToDateOnly(),
              DateTime.Now.AddDays(2).ToDateOnly(),
              false,
              326);

    public static EtebarDoreh Create(Guid etebarGuid)
       => EtebarDoreh.Create(
             etebarGuid,
             "DUMMY_Title_Create",
             DateTime.Now,
             Guid.NewGuid(),
             DateTime.Now.ToDateOnly(),
             DateTime.Now.AddDays(2).ToDateOnly(),
             false,
             326);
}
