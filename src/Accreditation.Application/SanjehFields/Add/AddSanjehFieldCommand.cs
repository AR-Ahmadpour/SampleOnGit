using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.SanjehFields.Add
{
    public sealed record AddSanjehFieldCommand( Guid FieldGuid, List<Guid> SanjehGuids)
     : ICommand<Guid>;


}
