using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.Mehvars.Dtos;

namespace Accreditation.Application.Mehvars.GetList;

public sealed record GetListMehvarQuery(Guid Etebardorehid, int PageNumber, int PageSize) :
    IQuery<PagedList<GetAllByEtebarDorehDto>>;



