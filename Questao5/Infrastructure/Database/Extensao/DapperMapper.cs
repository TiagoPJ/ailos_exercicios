using Dapper;
using System.ComponentModel;
using System.Reflection;

namespace Questao5.Infrastructure.Database.Extensao
{
    public class DapperMapper
    {
        public static void Mapper<T>()
        {
            var map = new CustomPropertyTypeMap(typeof(T),
                        (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop).Equals(columnName, StringComparison.InvariantCultureIgnoreCase)));
            SqlMapper.SetTypeMap(typeof(T), map);
        }

        static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;
            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return attrib == null ? member.Name : attrib.Description;
        }
    }
}
