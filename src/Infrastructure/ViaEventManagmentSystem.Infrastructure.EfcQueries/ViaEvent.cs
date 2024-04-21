using System;
using System.Collections.Generic;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries;

public partial class ViaEvent
{
    public string Id { get; set; } = null!;

    public string? EventTitle { get; set; }

    public string? Description { get; set; }

    public string? StartDateTime { get; set; }

    public string? EndDateTime { get; set; }

    public int? MaxNumberOfGuests { get; set; }

    public string? EventVisibility { get; set; }

    public string EventStatus { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
