using Common.EventBus.Abstractions;
using Common.EventBus.RabbitMQ;
using Tony.Web.Infrastructure.JwtUtil;
using Tony.Web.Infrastructure.RazorUtils;
using Tony.Web.Infrastructure.Services;

namespace Tony.Web.Infrastructure;

public static class RegisterDependencyServices
{
    public static IServiceCollection RegisterWebDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<IEventBus, EventBusRabbitMQ>();
        services.AddTransient<IRenderViewToString, RenderViewToString>();
        services.AddAutoMapper(typeof(RegisterDependencyServices).Assembly);
        services.AddScoped<IHomePageService, HomePageService>();

        return services;
    }
}