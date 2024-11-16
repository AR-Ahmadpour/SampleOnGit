using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.ZirMehvars.Dtos;

namespace Accreditation.Application.ZirMehvars.GetList;

public sealed record GetListZirMehvarQuery(Guid MehvarId, int PageNumber, int PageSize) :
IQuery<PagedList<GetListByMehvarIdAsyncDto>>;
