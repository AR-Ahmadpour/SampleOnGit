
using Accrediation.Application.Common.FluentValidationCustomLanguageManager;
using Accreditation.Application.Abstractions.Behaviors;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.EventBusService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Accreditation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IAuthenticationService, AuthenticationService>();
        //services.AddScoped<IDocumentService, DocumentService>();
        //services.AddScoped<IUserService, UserService>();

        //Needed to Add For IntegrationTest Tests
        //services.AddScoped<ICommandHandler<AddEtebarDorehCommand, Guid>, AddEtebarDorehCommandHandler>();
        //services.AddTransient<IQueryHandler<GetOrgTypeSelectListQuery,List<GetOrgTypeSelectListResponse>>, GetOrgTypeSelectListQueryHandler>();
        //services.AddScoped<IQueryHandler<GetListEtebarDorehQuery, IPaginator<GetListDto>>, GetListEtebarDorehQueryHandler>();
        //services.AddTransient<IQueryHandler<GetEtebarDorehByIdQuery, GetEtebarDorehResponse>, GetEtebarDorehByIdQueryHandler>();
        //services.AddScoped<ICommandHandler<EditEtebarDorehCommand, Guid>, EditEtebarDorehCommandHandler>();
        //services.AddTransient<ICommandHandler<AddMehvarCommand, Guid>, AddMehvarCommandHandler>();
        //services.AddTransient<IQueryHandler<GetListMehvarQuery, IPaginator<GetAllByEtebarDorehDto>>, GetListMehvarQueryHandler > ();

        //Eventbus services
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddLogging();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            //config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });


        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        ValidatorOptions.Global.LanguageManager = new FluentValidationCustomLanguage();


        return services;
    }
}


