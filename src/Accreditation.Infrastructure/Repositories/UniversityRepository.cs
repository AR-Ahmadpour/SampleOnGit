using Accreditation.Application.Common.Interfaces.Persistence.Universityes;
using Accreditation.Application.Universityes.GetAll;
using Accreditation.Domain.Universites.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class UniversityRepository(AccreditationDbContext context) : IUniversityRepository
{
    public async Task<List<SelectListResponse>> GetSelectListUniversityAsync(CancellationToken cancellationToken = default)
    {
        return await context.Universities
                 .Where(x => !x.IsDeleted)
                 .Select(x => new SelectListResponse(x.GUID, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }

    public async Task<List<GetAllUniversityDto>> GetUniversity(GetAllUniversityQuery query, CancellationToken cancellationToken)
    {
        return await context.Set<University>()
            .Select(_ => new GetAllUniversityDto
            {
                Id = _.Id,
                GUID = _.GUID,
                Title = _.Title,
            }).ToListAsync(cancellationToken);
    }

}
