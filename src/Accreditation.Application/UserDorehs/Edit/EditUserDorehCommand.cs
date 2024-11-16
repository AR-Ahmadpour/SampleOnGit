using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.UserDorehs.Edit
{
    public sealed record EditUserDorehCommand(Guid UserDorehId,Guid DorehAmoozeshiGuid,string DorehTitle,
        string BarGozarKonandeh, int DorehHours, bool DorehRole)
: ICommand<Guid>;
}
