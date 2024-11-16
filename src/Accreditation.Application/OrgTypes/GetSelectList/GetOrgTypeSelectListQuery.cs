using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.OrgTypes.GetSelectList;

public sealed record GetOrgTypeSelectListQuery() : IQuery<List<GetOrgTypeSelectListResponse>>;

