namespace Accreditation.Api.Endpoints.UserDorehs.Adds
{
   public sealed record AddUserDorehRequest(
       Guid UserGuid,
       Guid DorehAmoozeshiGuid,
       string DorehTitle,
       string BargozarKonnandeh,
       int DorehHours,
       bool DorehRole);
}
