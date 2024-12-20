using System.ComponentModel.DataAnnotations;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;

public class ViaGuest : Aggregate<GuestId>
{
    internal GuestId Id => base.Id;
    internal FirstName _FirstName { get; private set; }
    internal LastName _LastName { get; private set; }
    internal Email _Email { get; private set; }
    internal ViaEvent _ViaEvent { get; private set; }


    // EF Core will use this constructor
    private ViaGuest()
    {
    }

    public ViaGuest(GuestId Id) : base(Id)
    {
        _FirstName = null;
        _LastName = null;
        _Email = null;
        // _ReceivedInvitations = new List<Invitation>();
    }

    public ViaGuest(GuestId Id, FirstName firstName, LastName lastName, Email email) : base(Id)
    {
        _FirstName = firstName;
        _LastName = lastName;
        _Email = email;
    }


    public static Result<ViaGuest> Create(GuestId Id, FirstName firstName, LastName lastName, Email email)
    {
        return Result<ViaGuest>.Success(new ViaGuest(Id, firstName, lastName, email));
    }

    public static Result<ViaGuest> Create(GuestId Id)
    {
        return Result<ViaGuest>.Success(new ViaGuest(Id));
    }

    public Result<ViaGuest> UpdateEmail(Email email)
    {
        if (email == null)
            return Result<ViaGuest>.Failure(Error.AddCustomError("Email is null or invalid"));

        _Email = email;

        return Result<ViaGuest>.Success(this);
    }


    public Result<ViaGuest> UpdateFirstName(FirstName newFirstName)
    {
        if (newFirstName == null)
            return Result<ViaGuest>.Failure(Error.AddCustomError("FirstName is null"));
        _FirstName = newFirstName;

        return Result<ViaGuest>.Success(this);
    }

    public Result<ViaGuest> UpdateLastName(LastName newLastName)
    {
        if (newLastName == null)
            return Result<ViaGuest>.Failure(Error.AddCustomError("LastName is null"));
        _LastName = newLastName;
        return Result<ViaGuest>.Success(this);
    }
}