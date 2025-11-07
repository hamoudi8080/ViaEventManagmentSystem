using Microsoft.Extensions.DependencyInjection;
using ViaEventManagementSystem.Core.AppEntry.Commands;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Event;
using ViaEventManagementSystem.Core.Application.CommandHandlers.Features.Guest;

namespace ViaEventManagementSystem.Core.Application.Extension;

public static class ApplicationExtension
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        // Event Handlers
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, UpdateEventTitleHandler>();
        services.AddScoped<ICommandHandler<UpdateDescriptionCommand>, UpdateDescriptionHandler>();
        services.AddScoped<ICommandHandler<MakeEventReadyCommand>, MakeEventReadyHandler>();
        services.AddScoped<ICommandHandler<MakeEventPublicCommand>, MakeEventPublicHandler>();
        services.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateHandler>();
        services.AddScoped<ICommandHandler<ActivateEventCommand>, ActivateEventHandler>();
        services.AddScoped<ICommandHandler<UpdateEventMaxNoOfGuestsCommand>, UpdateEventMaxNoOfGuestsHandler>();
        services.AddScoped<ICommandHandler<InviteGuestCommand>, InviteGuestHandler>();
        services.AddScoped<ICommandHandler<AcceptInvitationCommand>, AcceptInvitationHandler>();
        services.AddScoped<ICommandHandler<DeclineInvitationCommand>, DeclineInvitationHandler>();
        services.AddScoped<ICommandHandler<ParticipateGuestCommand>, ParticipateGuestHandler>();
        services.AddScoped<ICommandHandler<GuestCancelsParticipationCommand>, GuestCancelsParticipationHandler>();

        // Guest Handlers
        services.AddScoped<ICommandHandler<CreateGuestCommand>, CreateGuestHandler>();

        return services;
    }
}