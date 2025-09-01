using System.Reflection;

namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Enumeration
{
    public int Value { get; private set; }
    public string DisplayName { get; private set; }

    protected Enumeration()
    {
    }

    protected Enumeration(int value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Enumeration))
        {
            return false;
        }

        var otherValue = (Enumeration)obj;
        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return DisplayName;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();
            var locatedValue = info.GetValue(instance) as T;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
    {
        var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }

    private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new InvalidOperationException(message);
        }

        return matchingItem;
    }
}
