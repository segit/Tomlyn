using System.Reflection;
using Tomlyn;

namespace MyTomlyn.Cmd;

public static class TomlModelOptionsProvider
{
    /// <summary>
    /// Delegate that returns the property name only if the property is of type Dictionary&lt;string, HostModel&gt;.
    /// Otherwise, returns null to skip deserialization for that property.
    /// </summary>
    public static string? GetPropertyNameOnlyForDictionary(PropertyInfo prop)
    {
        if (prop.PropertyType == typeof(Dictionary<string, HostModel>))
        {
            // Use the default conversion (snake_case) as in TomlModelOptions
            return TomlModelOptions.DefaultConvertPropertyName(prop.Name);
        }
        return null;
    }
}