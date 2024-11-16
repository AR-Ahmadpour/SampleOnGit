namespace Accreditation.Api.Endpoints.UserDorehs.Edits
{
    public sealed record EditUserDorehRequest(
    Guid DorehAmoozeshiGUID,
    string DorehTitle,
    string BargozarKonandeh,
    int DorehHours,
    bool DorehRole
    );
}
