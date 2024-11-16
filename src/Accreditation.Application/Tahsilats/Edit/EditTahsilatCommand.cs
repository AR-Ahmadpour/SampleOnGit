using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Tahsilats.Edit
{
    public sealed record EditTahsilatCommand(Guid TahsilatGuid, Guid ReshtehTahsiliGuid, Guid MaghtaTahsiliGuid,
      string? MadrakGuid, string UniversityName, string GraduationDate) : ICommand<Guid>;
}
