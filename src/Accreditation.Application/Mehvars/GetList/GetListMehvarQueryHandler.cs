using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Mehvars.Dtos;
using SharedKernel;

namespace Accreditation.Application.Mehvars.GetList;

internal sealed class GetListMehvarQueryHandler
    : IQueryHandler<GetListMehvarQuery, PagedList<GetAllByEtebarDorehDto>>
{
    private readonly IMehvarRepository _mehvarRepository;

    public GetListMehvarQueryHandler(IMehvarRepository mehvarRepository)
    {
        _mehvarRepository = mehvarRepository;
    }
    public async Task<Result<PagedList<GetAllByEtebarDorehDto>>> Handle(
        GetListMehvarQuery request,
        CancellationToken cancellationToken)
    {
        return await _mehvarRepository.GetAllByEtebarDorehIdAsync(
             request.Etebardorehid,
             request.PageNumber,
             request.PageSize,
             cancellationToken);

    }
}

