

using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using SharedKernel;


namespace Accreditation.Application.ZirMehvars.GetById;

internal sealed class GetZirMehvarByIdQueryHandler
    : IQueryHandler<GetZirMehvarByIdQuery, GetZirMehvarResponse>
{
    private readonly IZirMehvarRepository _zirMehvarRepository;

    public GetZirMehvarByIdQueryHandler(IZirMehvarRepository zirMehvarRepository)
    {
        _zirMehvarRepository = zirMehvarRepository;
    }
    public async Task<Result<GetZirMehvarResponse>> Handle(GetZirMehvarByIdQuery request, CancellationToken cancellationToken)
    {


        var zirmehvar = await _zirMehvarRepository.GetByIdAsync(request.GUID, cancellationToken);

        if (zirmehvar == null)
        {
            return Result.Failure<GetZirMehvarResponse>(ZirMehvarErrors.NotFound);
        }


        var response = new GetZirMehvarResponse
        {
            Guid = zirmehvar.GUID,
            Title = zirmehvar.Title,
            SortOrder = zirmehvar.SortOrder,
            WeightedCoefficient = zirmehvar.WeightedCoefficient
        };

        return response;

    }


}


