namespace Accreditation.Api.Endpoints.Users.LogIns;

public sealed record LogInUserRequest(string NationalCode, string Password);
