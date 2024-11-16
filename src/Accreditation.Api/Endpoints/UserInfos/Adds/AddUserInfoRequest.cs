namespace Accreditation.Api.Endpoints.UserInfos.Adds
{
    public sealed record AddUserInfoRequest(
    Guid UserGuid,
    string Address,
    bool MaritalStatus,
    int BirthOstandId,
    int BirthShahrId,
    int AddressOstanId,
    int AddressShahrId,
    int ChildCount,
    string PersonalPicGUID,
    string KartMeliGUID,
    string ShenasnamehGUID
    );
}
