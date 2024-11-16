using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.HamkariModels.GetList
{
    public sealed record GetListHamkariModelQuery():
        IQuery<List<GetListHamkariModelDto>>;
}
