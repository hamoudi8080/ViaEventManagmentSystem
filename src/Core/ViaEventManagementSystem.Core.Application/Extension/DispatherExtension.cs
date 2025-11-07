using Microsoft.Extensions.DependencyInjection;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher;

namespace ViaEventManagementSystem.Core.Application.Extension;

public static class DispatcherExtension
{
    public static IServiceCollection RegisterDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        return services;
    }
}
