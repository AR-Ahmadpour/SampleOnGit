namespace Accreditation.Api.Endpoints.Tahsilat.Edits
{
    public sealed record EditTahsilatRequest(
    Guid ReshtehTahsiliGuid,
    Guid MaghtaTahsiliGuid,
    string? MadrakGuid,
    string UniversityName,
    string GraduationDate
    );
}
