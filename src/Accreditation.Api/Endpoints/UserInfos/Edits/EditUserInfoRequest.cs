namespace Accreditation.Api.Endpoints.UserInfos.Edits
{
    public sealed record EditUserInfoRequest(
        string Address,
        int BirthOstanId,
        int BirthShahrId,
        int AddressOstanId,
        int AddressShahrId,
        bool MaritalStatus,
        int ChildCount,
        string? PersonalPicGUID,
        string? KartMeliGUID,
        string? ShenasnamehGUID
        ); 
}
