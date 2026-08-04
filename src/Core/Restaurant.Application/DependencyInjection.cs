using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Behaviors;

namespace Restaurant.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                //config.AddOpenBehavior(typeof(AuditLogBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // AuditContext: Scoped — sống trong 1 request, chia sẻ giữa Behavior và DbContext
            //services.AddScoped<AuditContext>();

            return services;
        }
    }
}
