namespace DDDYTSample.Contracts.Authentication
{
    public record RegisterRequest(
        string Email,
        string Password,
        string ConfirmPassword);
}
