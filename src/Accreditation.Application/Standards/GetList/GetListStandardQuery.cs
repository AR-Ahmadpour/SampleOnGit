using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Standards.Dtos;


namespace Accreditation.Application.Standards.GetList
{
    public sealed record GetListStandardQuery(Guid ZirMehvarid, int PageNumber, int PageSize) :
    IQuery<PagedList<GetAllByZirMehvarIdAsyncDto>>;
}
