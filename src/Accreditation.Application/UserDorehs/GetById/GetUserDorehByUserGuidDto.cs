namespace Accreditation.Application.UserDorehs.GetById
{
    public sealed class GetUserDorehByUserGuidDto
    {
        public Guid Guid { get; set; }
        public Guid DorehAmoozeshiGUID { get; set; }
        public string DorehAmoozeshiTitle { get; set; }
        public string DorehTitle { get; set; }
        public string BargozarKonandeh { get; set; }
        public int DorehHours { get; set; }
        public bool DorehRole { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
    }
}
