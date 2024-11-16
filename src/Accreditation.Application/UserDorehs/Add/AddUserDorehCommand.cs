using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.UserDorehs.Add
{
    public sealed record AddUserDorehCommand(Guid UserGuid, Guid DorehAmoozeshiGuid, string DorehTitle,
        string BargozarKonandeh, int DorehHours, bool DorehRole)
  : ICommand<Guid>;
}
