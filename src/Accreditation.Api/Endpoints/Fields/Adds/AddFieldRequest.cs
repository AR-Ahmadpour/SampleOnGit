namespace Accreditation.Api.Endpoints.Fields.Adds
{
    public sealed record AddFieldRequest(
        Guid EtebarDorehGuid,
        string Title,
        string TitleCode,
        List<string> InstanceTypeIds
        );
}
