namespace Accreditation.Application.UserInfos.GetById
{
    public sealed class GetUserInfoByUserGuidDto
    {
        public Guid Guid { get; set;}
        public bool MaritalStatus { get; set;}
        public int ChildCount { get; set;}
        public int BirthOstandId { get; set;}
        public int BirthShahrId { get; set;}
        public int AddressOstanId { get; set;}
        public int AddressShahrId { get; set;}
        public string Address { get; set;}
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? NationalCOde { get; set;}
        public string? PersonalPicGUID { get; set;}
        public string? KartMeliGUID { get; set;}
        public string? ShenasnamehGUID { get; set;}

    }
}
