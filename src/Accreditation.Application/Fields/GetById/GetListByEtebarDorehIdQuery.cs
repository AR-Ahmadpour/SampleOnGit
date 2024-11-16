using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Fields.GetList;

namespace Accreditation.Application.Fields.GetById
{
    public sealed record GetlistByEtebarDorehIdQuery(Guid EtebardorehGuid,int PageNumber,int Pagesize) :
                                    IQuery<PagedList<GetListByEtebarDorehIdDto>>;
}
