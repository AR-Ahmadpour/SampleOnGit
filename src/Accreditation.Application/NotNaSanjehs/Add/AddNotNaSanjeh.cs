using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.NotNaSanjehs.Add
{
    public sealed record AddNotNaSanjehCommand(Guid SanjehGuid,
     List<Guid> OrgGerayeshGuids)
      : ICommand;
}
