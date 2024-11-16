using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Tahsilats.Add
{
    public sealed record AddTahsilatCommand(Guid UserGuid, Guid ReshtehTahsiliGuid, Guid MaghtaTahsiliGuid,
      string? MadrakGuid, string UniversityName, string GraduationDate) : ICommand<Guid>;


}
