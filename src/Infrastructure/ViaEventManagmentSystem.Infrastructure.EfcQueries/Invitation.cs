using System;
using System.Collections.Generic;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public int InvitationStatus { get; set; }

    public string EventId { get; set; } = null!;

    public string GuestId { get; set; } = null!;

    public virtual ViaEvent Event { get; set; } = null!;
}
