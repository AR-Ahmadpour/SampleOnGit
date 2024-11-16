namespace Accreditation.Application.Organization.GetList
{
    public class GetListOrganizationDto
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? GerayeshName { get; set; }
        public string? OrgTypeName { get; set; }
        public string? SharestanName { get; set; }
        public string? UniversityName { get; set; }
        public bool IsTatilDaem { get; set; }
    }
}
