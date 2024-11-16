using Accreditation.Domain.AccInstanceMehvars.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;

public interface IAccInstanceMehvarRepository
{
    Task Add(AccInstanceMehvar accInstanceMehvar);
    Task<List<AccInstanceMehvar>> GetListAccInstanceMehvarAsync(Guid accInstanceGuid);
    void Delete(AccInstanceMehvar accInstanceMehvar);
    Task<AccInstanceMehvar?> Find(Guid Id);
}
