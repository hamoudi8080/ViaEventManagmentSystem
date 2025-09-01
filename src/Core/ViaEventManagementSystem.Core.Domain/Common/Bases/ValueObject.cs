namespace ViaEventManagementSystem.Core.Domain.Common.Bases;


public abstract class ValueObject
{
    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;

        return ((ValueObject)other).GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    
    //Why Implement GetHashCode():
    //When you override the Equals() method to provide custom equality comparison for your objects, 
    // it's also recommended to override the GetHashCode() method.
    // This is because if two objects are considered equal (according to the Equals() method),
    // they must return the same hash code when calling GetHashCode().
    // This ensures consistency and correctness when using these objects in collections like dictionaries.
    //for example This ensures that if two Person objects are equal,
    //they will have the same hash code, allowing them to be correctly stored and retrieved from collections.
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(obj => obj?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}
