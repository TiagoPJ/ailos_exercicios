using System.ComponentModel;

namespace Questao5.Utils.Enum
{
    public static class Enumextensao
    {
        public static string ToDescriptionString(this System.Enum enumValue)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])enumValue
               .GetType()
               .GetField(enumValue.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
