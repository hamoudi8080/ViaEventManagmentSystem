﻿namespace ViaEventManagmentSystem.Core.Domain.Common.Bases;

public abstract class Entity<TId>
{
    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
        throw new NotImplementedException();
    }


    /*
    // Override Equals and GetHashCode for entity comparison based on identity
    public override bool Equals(object obj)
    {
        if (obj is null || !(obj is Entity<TId> otherEntity))
            return false;

        // Entities are considered equal if their types are the same and their IDs are equal
        return GetType() == otherEntity.GetType() && EqualityComparer<TId>.Default.Equals(Id, otherEntity.Id);
    }

    public override int GetHashCode()
    {
        // Use the hash code of the entity's ID for consistency
        return Id.GetHashCode();
    }
    */
}