

using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.EnvironmentStandards.GetList
{
    public sealed record GetListEnvironmentStandardQuery(Guid etebarDorehGuid) :
     IQuery<List<GetListByEtebarDorehDto>>;
}
