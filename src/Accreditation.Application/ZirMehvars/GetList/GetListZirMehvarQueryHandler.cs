using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Models;
using Accreditation.Application.ZirMehvars.Dtos;
using SharedKernel;

namespace Accreditation.Application.ZirMehvars.GetList;

internal sealed class GetListZirMehvarQueryHandler
: IQueryHandler<GetListZirMehvarQuery, PagedList<GetListByMehvarIdAsyncDto>>
{
    private readonly IZirMehvarRepository _zirMehvarRepository;
    

    public GetListZirMehvarQueryHandler(IZirMehvarRepository zirMehvarRepository)
    {
        _zirMehvarRepository = zirMehvarRepository;
    }
    public async Task<Result<PagedList<GetListByMehvarIdAsyncDto>>> Handle(GetListZirMehvarQuery request, CancellationToken cancellationToken)
    {
        return await _zirMehvarRepository.GetListByMehvarIdAsync(request.MehvarId, request.PageNumber, request.PageSize, cancellationToken);

    }
}
