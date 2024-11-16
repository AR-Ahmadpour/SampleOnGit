namespace Accreditation.Application.Tahsilats.GetById
{
    public sealed class GetTahsilatByUserGuidDto
    {
        public Guid Guid { get; set; }
        public Guid MaghtaTahsili { get; set; }
        public Guid ReshtehTahsili { get; set; }
        public string GraduationDate { get; set; }
        public string UniversityName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCOde { get; set; }
        public string? MadrakGUID { get; set; }

    }
}
