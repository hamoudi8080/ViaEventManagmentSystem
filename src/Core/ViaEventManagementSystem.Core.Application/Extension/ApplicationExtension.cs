using Microsoft.Extensions.DependencyInjection;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;

namespace ViaEventManagementSystem.Core.Application.Extension;

public class ApplicationExtension
{
    public static void RegisterHandler(IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, UpdateEventTitleHandler>();
        services.AddScoped<ICommandHandler<UpdateDescriptionCommand>, UpdateDescriptionHandler>();
        // Add more handlers here
    }
}