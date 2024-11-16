using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Fields.Add
{
    public sealed record AddFieldCommand(Guid EtebarDorehGuid,
        string Title,
         string TitleCode,
        List<string> InstanceTypeIds)
     : ICommand<Guid>;
}
