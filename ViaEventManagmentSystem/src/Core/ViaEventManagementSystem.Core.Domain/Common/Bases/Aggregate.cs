namespace ViaEventManagementSystem.Core.Domain.Common.Bases;

public abstract class Aggregate<TId> : Entity<TId>
{
    protected Aggregate(TId id) : base(id)
    {
    }

    //For serialization, EFC

    protected Aggregate()
    {
    }
}