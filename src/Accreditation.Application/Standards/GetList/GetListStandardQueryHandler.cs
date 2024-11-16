using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Standards.Dtos;
using SharedKernel;

namespace Accreditation.Application.Standards.GetList;

internal sealed class GetListStandardQueryHandler :
    IQueryHandler<GetListStandardQuery, PagedList<GetAllByZirMehvarIdAsyncDto>>
{
    private readonly IStandardRepository _standardRepository;

    public GetListStandardQueryHandler(IStandardRepository standardRepository)
    {
        _standardRepository = standardRepository;
    }
    public async Task<Result<PagedList<GetAllByZirMehvarIdAsyncDto>>> Handle(GetListStandardQuery request, CancellationToken cancellationToken)
    {
        return await _standardRepository.GetListByZirMehvarIdAsync(request.ZirMehvarid,
                                                                   request.PageNumber,
                                                                   request.PageSize,
                                                                   cancellationToken);
     }
}
