using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GraphQlClient
{
    internal class QueryBuilder
    {
        private  readonly NamingStrategy _strategy;

        public QueryBuilder(NamingStrategy strategy)
        {
            _strategy = strategy;
        }

        public string CreateRetrieveQuery<T>(T query) =>
            new StringBuilder()
                .Append('{')
                .Append(AddProperties(query))
                .Append('}')
                .ToString();

        private string AddProperties<T>(T thing)
        {
            var sb = new StringBuilder();

            var properties = thing.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(thing, null);
                var type = prop.PropertyType;

                var name = _strategy.Name(prop);

                if (type.IsValueType)
                {

                    sb.Append($"{name},");
                }
                else if (type == typeof(string))
                {
                    sb.Append($"{name},");
                }
                else if (type.IsClass)
                {
                    var typedPropertyVal = Convert.ChangeType(propValue, prop.PropertyType);
                    sb.Append($"{name} {{ {AddProperties(typedPropertyVal)} }}");
                }
            }

            return sb.ToString();
        }
    }

    public abstract class NamingStrategy
    {
        public abstract string Name(PropertyInfo prop);
    }

    public class CamelCaseNamingStrategy : NamingStrategy
    {
        public override string Name(PropertyInfo prop)
        {
            return char.ToLower(prop.Name[0]) + string.Join("", prop.Name.Skip(1));
        }
    }
}