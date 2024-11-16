using Accreditation.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Users.GetPermisionByCategory
{
    public sealed record GetPermisionByCategoryQuery(int CategoryID):IQuery<List<GetPermisionByCategoryDto>>;
  
}
