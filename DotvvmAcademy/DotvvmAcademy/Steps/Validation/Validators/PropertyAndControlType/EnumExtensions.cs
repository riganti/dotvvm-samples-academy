using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this ControlBindName val)
        {
            var descriptionAttribute =
                (DescriptionAttribute) typeof(ControlBindName).GetTypeInfo().GetField(val.ToString())
                    .GetCustomAttribute(typeof(DescriptionAttribute));
            return descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;
        }
    }
}