using Accreditation.Domain.AccInstanceStandards.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccInstanceStandards;
public interface IAccInstanceStandardRepository
{
    Task Add(AccInstanceStandard accInstanceStandard);
    Task<List<AccInstanceStandard>> FindByAccInstanceGuid(Guid accInstanceGuid);
    Task<AccInstanceStandard> FindByAccInstanceAndAcczirmehvarGuid(Guid accInstanceGuid, Guid accInsZirMehvarGuid);
    void Delete(AccInstanceStandard accInstanceStandard);
}
