using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CottonPrompt.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            // Get the member info for the enum value
            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString())[0];

            // Attempt to retrieve the Display attribute
            var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();

            // Return the Name property of the Display attribute if it exists; otherwise, use the enum's ToString
            return displayAttribute?.GetName() ?? enumValue.ToString();
        }

        public static T GetEnumFromName<T>(string name) where T : Enum
        {
            // Check if the name matches any enum names directly
            if (Enum.TryParse(typeof(T), name, true, out var result))
            {
                return (T)result;
            }

            // If not, check against the Display attributes
            foreach (var field in typeof(T).GetFields())
            {
                var attribute = field.GetCustomAttribute<DisplayAttribute>();
                if (attribute != null && attribute.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            // If no match is found, throw an exception
            throw new ArgumentException($"No enum value with name or display name '{name}' found for {typeof(T)}.");
        }
    }
}
