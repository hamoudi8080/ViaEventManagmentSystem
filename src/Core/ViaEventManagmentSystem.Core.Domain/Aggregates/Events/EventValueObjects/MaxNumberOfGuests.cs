﻿using System.ComponentModel.DataAnnotations;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class MaxNumberOfGuests : ValueObject
{
    public int Value { get; private init; }


    private MaxNumberOfGuests(int maxNumber)
    {
        Value = maxNumber;
    }

    public static Result<MaxNumberOfGuests> Create(int maxNumber)
    {

        if (!Validate(maxNumber))
        {
            return Result<MaxNumberOfGuests>.Failure(Error.BadRequest(ErrorMessage.MaxGuestsNoMustBeWithin5and50));
        }
        
        return Result<MaxNumberOfGuests>.Success(new MaxNumberOfGuests(maxNumber));
    }

    private static bool Validate(int maxNumber)
    {
        if (maxNumber <5 || maxNumber > 50)
        {
            return false;
        }

        return true;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}