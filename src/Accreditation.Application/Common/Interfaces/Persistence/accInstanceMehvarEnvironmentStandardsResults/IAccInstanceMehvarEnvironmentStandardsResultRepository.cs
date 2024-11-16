using Accreditation.Domain.AccInstanceMehvarEnvironmentStandardsResults.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;

public interface IAccInstanceMehvarEnvironmentStandardsResultRepository
{
    void Add(AccInstanceMehvarEnvironmentStandardsResult accInstanceMehvarResult);
    Task<List<AccInstanceMehvarEnvironmentStandardsResult>> GetListAccInstanceMehvarAsync(Guid accInstanceMehvarGUID);
    void Delete(AccInstanceMehvarEnvironmentStandardsResult accInstanceMehvarResult);
}
