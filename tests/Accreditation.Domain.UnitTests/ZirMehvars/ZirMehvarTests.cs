using Accreditation.Domain.UnitTests.Infrastructure;
using Accreditation.Domain.ZirMehvars.Entities;
using FluentAssertions;

namespace Accreditation.Domain.UnitTests.ZirMehvars;

public class ZirMehvarTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        ZirMehvar zirMehvar = CreateZirMehvar();

        // Assert
        zirMehvar.MehvarGUID.Should().Be(ZirMehvarData.MehvarGUID);
        zirMehvar.Title.Should().Be(ZirMehvarData.Title);
        zirMehvar.CreationDate.Should().Be(ZirMehvarData.CreationDate);
        zirMehvar.CreatedByGUID.Should().Be(ZirMehvarData.CreatedByGUID);
        zirMehvar.IsDeleted.Should().Be(ZirMehvarData.IsDeleted);
        zirMehvar.SortOrder.Should().Be(ZirMehvarData.SortOrder);
        zirMehvar.WeightedCoefficient.Should().Be(ZirMehvarData.WeightedCoefficient);
        zirMehvar.UpdateDate.Should().BeNull();
        zirMehvar.UpdatedByGUID.Should().BeNull();
    }

    private static ZirMehvar CreateZirMehvar()
    {
        return ZirMehvar.Create(
         ZirMehvarData.MehvarGUID, ZirMehvarData.Title, ZirMehvarData.SortOrder,
         ZirMehvarData.CreatedByGUID, ZirMehvarData.CreationDate, ZirMehvarData.WeightedCoefficient);
    }
}

