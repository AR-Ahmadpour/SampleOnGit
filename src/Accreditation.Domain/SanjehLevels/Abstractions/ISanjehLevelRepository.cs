

using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.SanjehLevels.Dtos;
using Accreditation.Domain.SanjehLevels.Entities;


namespace Accreditation.Domain.SanjehLevels.Abstractions;

public interface ISanjehLevelRepository
{
    Task<List<GetAllSanjehLevelDto>> GetAllSanjehLevel(CancellationToken cancellationToken = default);

    Task<SanjehLevel?> FindAsync(int SanjehLevelId, CancellationToken cancellationToken = default);
}
