using Accreditation.Domain.AccInstanceMehvars.Entities;
using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using MediatR;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;

public interface IAccInstanceZirMehvarRepository
{
    Task Add(AccInstanceZirMehvar accInstanceZirMehvar);
    Task<List<AccInstanceZirMehvar>> GetListAccInstanceZirMehvarAsync(Guid accInstanceGuid);
    void Delete(AccInstanceZirMehvar accInstanceMehvar);
}