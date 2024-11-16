namespace Accreditation.Api.Endpoints.Tahsilat.Adds
{
    public sealed record AddTahsilatRequest(
    Guid UserGuid,
    Guid ReshtehTahsiliGuid,
    Guid MaghtaTahsiliGuid,
    string? MadrakGuid,
    string UniversityName,
    string GraduationDate
    );
}
