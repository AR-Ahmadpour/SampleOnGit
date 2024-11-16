using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Accrediation.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            //todo :
            //  : Result.Failure(result.Errors.Select(e => e.Description));
            : Result.Failure(new Error("401", "Unauthorized  ", ErrorType.Validation));
    }
}
