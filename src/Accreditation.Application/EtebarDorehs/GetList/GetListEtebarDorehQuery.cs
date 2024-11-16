using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.EtebarDorehs.Dtos;

namespace Accreditation.Application.EtebarDorehs.GetList;

public sealed record GetListEtebarDorehQuery(int PageNumber, int PageSize, Guid? Orgtype) :
    IQuery<PagedList<GetListDto>>;
