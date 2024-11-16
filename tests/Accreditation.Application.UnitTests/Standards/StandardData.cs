using Accreditation.Domain.Standards.Entities;
using System.Net;
using System.Xml.Linq;

namespace Accreditation.Application.UnitTests.Standards;

internal static class StandardData
{
    public static Standard Create()
        => Standard.Create(
              Guid.NewGuid(),
              "DUMMY_Title_Create",
              "DUMMY_ShortTitle_Create",
              "124",
              326,
              Guid.NewGuid(),
              DateTime.Now,
              326);
}
