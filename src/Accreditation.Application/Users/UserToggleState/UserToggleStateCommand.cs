using Accreditation.Application.Abstractions.Messaging;


namespace Accreditation.Application.Users.UserToggleState
{ 
    public sealed record UserToggleStateCommand (Guid Uid): ICommand;
}