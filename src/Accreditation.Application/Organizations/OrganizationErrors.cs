using SharedKernel;

namespace Accreditation.Application.Organizations;
public static class OrganizationErrors
{
    public static Error NotFound => Error.NotFound("Organization.NotFound", "موسسه مورد نظر یافت نشد");
}
