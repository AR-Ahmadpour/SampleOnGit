using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Fields.Edit
{
    public sealed record EditFieldCommand(Guid GUID,
string Title, string TitleCode, List<string> InstanceTypeIds) : ICommand<Guid>;
}
