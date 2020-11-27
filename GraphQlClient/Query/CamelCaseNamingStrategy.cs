using System.Linq;
using System.Reflection;

namespace GraphQlClient.Query
{
    public class CamelCaseNamingStrategy : NamingStrategy
    {
        public override string Name(PropertyInfo prop)
        {
            return char.ToLower(prop.Name[0]) + string.Join("", prop.Name.Skip(1));
        }
    }
}