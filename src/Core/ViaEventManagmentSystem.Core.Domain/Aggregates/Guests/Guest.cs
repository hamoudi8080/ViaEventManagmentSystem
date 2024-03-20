﻿ 
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;

public class Guest : Aggregate<GuestId>
{
    internal GuestId _Id { get; private set; }
    internal FirstName _FirstName { get; private set; }
    internal LastName _LastName { get; private set; }
    internal Email _Email { get; private set; }
  

    internal List<Invitation> _ReceivedInvitations { get; private set; }
    public Guest(GuestId Id) : base(Id)
    {
        _FirstName = null;
        _LastName = null;
        _Email = null;
        _ReceivedInvitations = new List<Invitation>();
    }

    public Guest(GuestId Id,FirstName firstName, LastName lastName, Email email)
    {
        _Id = Id;
        _FirstName = firstName;
        _LastName = lastName;
        _Email = email;
        _ReceivedInvitations = new List<Invitation>();
    }

  

    public static Result<Guest> Create(GuestId Id, string firstName, string lastName, string email)
    {
        var _firstname = FirstName.Create(firstName);
        var _email = Email.Create(email);
        var _lastName = LastName.Create(lastName);
        if (!_firstname.IsSuccess)
        {
            return Result<Guest>.Failure(_firstname.Error);
        }  
        
        if (!_email.IsSuccess)
        {
            return Result<Guest>.Failure(_email.Error);
        }  
        
        if (!_lastName.IsSuccess)
        {
            return Result<Guest>.Failure(_lastName.Error);
        } 
        return Result<Guest>.Success(new Guest(Id, _firstname.Payload, _lastName.Payload , _email.Payload));
    }
    
    public static Result<Guest> Create(GuestId Id)
    {
        return Result<Guest>.Success(new Guest(Id));
    }

    public Result<Guest> UpdateEmail(Email email)
    {
        if (email == null)
            return Result<Guest>.Failure(Error.AddCustomError("Email is null"));

        return Result<Guest>.Success(this);
    }


    public void UpdateFirstName(string newFirstName)
    {
        if (string.IsNullOrWhiteSpace(newFirstName))
            throw new ArgumentException("First name cannot be null or empty.");
    }

    public void UpdateLastName(string newLastName)
    {
        if (string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentException("Last name cannot be null or empty.");
    }

 
    
    public void ReceiveInvitation(Invitation invitation)
    {
        _ReceivedInvitations.Add(invitation);
    }

    public void AcceptInvitation(Invitation invitation, ViaEvent viaEvent)
    {
        if (_ReceivedInvitations.Contains(invitation))
        {
            invitation.Accept();
            viaEvent.AddGuestParticipation(_Id);
        }
        else
        {
            throw new InvalidOperationException("Cannot accept an invitation that has not been received.");
        }
    }


    public void RejectInvitation(Invitation invitation)
    {
        if (_ReceivedInvitations.Contains(invitation))
        {
            invitation.Decline();
        }
        else
        {
            throw new InvalidOperationException("Cannot reject an invitation that has not been received.");
        }
    }
}