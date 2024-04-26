﻿using System;
using System.Collections.Generic;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries;

public partial class Guest
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ViaEvent> Events { get; set; } = new List<ViaEvent>();
}
