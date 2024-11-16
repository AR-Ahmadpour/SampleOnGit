using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.UnitTests.ZirMehvars;

internal static class ZirMehvarData
{ 
    public static ZirMehvar Create()
        => ZirMehvar.Create(
              Guid.NewGuid(),
              "DUMMY_Title_Create",
              36,
              Guid.NewGuid(),
              DateTime.Now,
              326);

    public static ZirMehvar Create(Guid id)
      => ZirMehvar.Create(
            id,
            "DUMMY_Title_Create",
            36,
            Guid.NewGuid(),
            DateTime.Now,
            326);
}
