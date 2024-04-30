using Microsoft.Extensions.DependencyInjection;
using ViaEventManagmentSystem.Core.AppEntry.Commands;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Application.CommandHandlers.Features.Event;

namespace ViaEventManagmentSystem.Core.Application.Extension;

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