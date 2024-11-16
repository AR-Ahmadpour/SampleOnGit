using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.Mehvars.Entities;

namespace Accreditation.Application.UnitTests.Mehvars;

internal static class MehvarData
{
    public static Mehvar Create()
        => Mehvar.Create(
              Guid.NewGuid(),
              "DUMMY_Title_Create",
              36,
              Guid.NewGuid(),
              DateTime.Now,
              326);
    public static Mehvar Create(Guid id)
      => Mehvar.Create(
             id,
            "DUMMY_Title_Create",
            36,
            Guid.NewGuid(),
            DateTime.Now,
            326);
}
