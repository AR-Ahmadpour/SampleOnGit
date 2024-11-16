namespace Accreditation.Application.Headers.GetList
{
    public sealed class GetHeaderDto
    {
        public string? UniversityName { get; set; }
        public string? OrgTypeTitle { get; set; }
        public string? FieldTitle { get; set; }
        public string? OrganizationName { get; set; }
        public string? ShahrTitle { get; set; }
        public string? ArzyabCredentials { get; set; }
        public string? EtebarDorehTitle { get; set; }
        public Guid? UserGuid { get; set; }
        public Guid? OrganizationGuid { get; set; }
        public Guid? FieldGuid { get; set; }
        public Guid? AccreditationInstanceGuid { get; set; }
        
           
    }
}
