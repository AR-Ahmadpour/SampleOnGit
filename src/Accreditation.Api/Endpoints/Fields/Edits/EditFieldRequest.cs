namespace Accreditation.Api.Endpoints.Fields.Edits
{
    public sealed record EditFieldRequest(
        string Title,
        string TitleCode,
        List<string> InstanceTypeIds
        );
}
