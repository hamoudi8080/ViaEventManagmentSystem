namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.Utilies;

public class MId<T> 
{
    
    public T Value { get; }

    public MId(T value)
    {
        if (typeof(T) != typeof(Guid))
        {
            throw new InvalidOperationException("Invalid type parameter. Only Guid is allowed.");
        }

        Value = value;
    }

    public static MId<T> Create() => new MId<T>((T)(object)Guid.NewGuid());

    public static MId<T> FromGuid(Guid guid) => new MId<T>((T)(object)guid);
}