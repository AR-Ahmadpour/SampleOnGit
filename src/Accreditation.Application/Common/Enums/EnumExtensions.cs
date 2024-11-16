using System.ComponentModel;
using System.Reflection;

namespace Accreditation.Domain.Common.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString(); // Fallback if no description is found
    }
}
