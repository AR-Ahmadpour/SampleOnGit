using Accreditation.Application.AccreditationalInstanceAnswers.Edit;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;
using Accreditation.Domain.AccreditationInstanceAnswers.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using SharedKernel;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccreditationInstanceAnswerRepository(AccreditationDbContext _context) : IAccreditationInstanceAnswerRepository
{
    public void Add(AccreditationalInstanceAnswer accreditationalInstanceAnswer)
    {
        _context.AddAsync(accreditationalInstanceAnswer);
    }

    public async Task<List<AccreditationalInstanceAnswer>> GetListAccInstanceAnswersAsync(Guid accreditationalInstanceGuid)
    {
        return await _context.AccreditationalInstanceAnswers
                 .Where(x => x.AccreditationInstanceGUID == accreditationalInstanceGuid)
                 .AsNoTracking()
                .ToListAsync();
    }

    public void Delete(AccreditationalInstanceAnswer accreditationalInstanceAnswer)
    {
        _context.AccreditationalInstanceAnswers.Remove(accreditationalInstanceAnswer);
    }

    public async Task<AccreditationalInstanceAnswer?> FindAsyc(Guid GUID, CancellationToken cancellationToken)
    {
        return await _context.AccreditationalInstanceAnswers.FindAsync(GUID, cancellationToken);
    }

    //public async Task<Guid> EdirResult(EditAccreditationalInstanceAnswerCommand command, CancellationToken cancellationToken)
    //{       
    //    return command.AccInsAnswerId;
    //}
}
