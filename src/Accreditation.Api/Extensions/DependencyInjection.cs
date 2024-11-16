using Accreditation.Api.Infrastructure;
using Accreditation.Api.OpenApi;
using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.EtebarDorehs.Add;
using Accreditation.Infrastructure.Authentication;
using Asp.Versioning;

namespace Accreditation.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpContextAccessor();

        //F aDDed
        // services.AddScoped<IUserContext, UserContext>();
        // REMARK: If you want to use Controllers, you'll need this.
        services.AddControllers();
       
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.ConfigureOptions<ConfigureSwaggerGenOptions>();

        return services;
    }
}
