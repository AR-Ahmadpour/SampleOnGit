using Accreditation.Domain.EtebarDorehs.Entities;
using Accreditation.Domain.EtebarDorehs.Events;
using Accreditation.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace Accreditation.Domain.UnitTests.EtebarDorehs;

public class ZirMehvarTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        EtebarDoreh EtebarDoreh = CreateEtebarDoreh();

        // Assert
        EtebarDoreh.OrgTypeGUID.Should().Be(ZirMehvarData.OrgTypeGUID);
        EtebarDoreh.Title.Should().Be(ZirMehvarData.Title);
        EtebarDoreh.CreationDate.Should().Be(ZirMehvarData.CreationDate);
        EtebarDoreh.StartDate.Should().Be(ZirMehvarData.StartDate);
        EtebarDoreh.EndDate.Should().Be(ZirMehvarData.EndDate);
        EtebarDoreh.CreatedByGUID.Should().Be(ZirMehvarData.CreatedByGUID);
        EtebarDoreh.IsCurrent.Should().Be(ZirMehvarData.IsCurrent);
        EtebarDoreh.IsDeleted.Should().Be(ZirMehvarData.IsDeleted);
        EtebarDoreh.SortOrder.Should().Be(ZirMehvarData.SortOrder);
        EtebarDoreh.UpdateDate.Should().BeNull();
        EtebarDoreh.UpdatedByGUID.Should().BeNull();
    }

    [Fact]
    public void Create_Should_RaiseEtebarDorehCreatedDomainEvent()
    {
        // Act
        EtebarDoreh EtebarDoreh = CreateEtebarDoreh();

        // Assert
        EtebarDorehDomainEvent EtebarDorehCreatedDomainEvent = AssertDomainEventWasPublished<EtebarDorehDomainEvent>(EtebarDoreh);

        EtebarDorehCreatedDomainEvent.Guid.Should().Be(EtebarDoreh.GUID);
    }

    private static EtebarDoreh CreateEtebarDoreh()
    {
        return EtebarDoreh.Create(
         ZirMehvarData.OrgTypeGUID, ZirMehvarData.Title, ZirMehvarData.CreationDate,
         ZirMehvarData.CreatedByGUID, ZirMehvarData.StartDate, ZirMehvarData.EndDate,
         ZirMehvarData.IsCurrent, ZirMehvarData.SortOrder);
    }
}

