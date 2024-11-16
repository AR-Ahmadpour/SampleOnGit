using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Accreditation.Domain.UnitTests.Mehvars;

public class MehvarTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        Mehvar Mehvar = CreateMehvar();

        // Assert
        Mehvar.Title.Should().Be(MehvarData.Title);
        Mehvar.EtebarDorehGUID.Should().Be(MehvarData.EtebarDorehGUID);
        Mehvar.SortOrder.Should().Be(MehvarData.SortOrder);
        Mehvar.WeightedCoefficient.Should().Be(MehvarData.WeightedCoefficient);
        Mehvar.IsDeleted.Should().Be(MehvarData.IsDeleted);
        Mehvar.CreationDate.Should().Be(MehvarData.CreationDate);
        Mehvar.UpdateDate.Should().BeNull();
        Mehvar.CreatedByGUID.Should().Be(MehvarData.CreatedByGUID);
        Mehvar.UpdatedByGUID.Should().BeNull();
    }

    private static Mehvar CreateMehvar()
    {
        return Mehvar.Create(MehvarData.EtebarDorehGUID, MehvarData.Title,
         MehvarData.SortOrder, MehvarData.CreatedByGUID, MehvarData.CreationDate,
         MehvarData.WeightedCoefficient);
    }
}

