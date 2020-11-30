using System.Linq;
using System.Reflection;
using GraphQlClient.Attributes;

namespace GraphQlClient.Query.NameRetriever
{
    public class ArgumentNameRetriever : AbstractNameRetriever
    {
        private readonly NamingStrategy _strategy;

        public ArgumentNameRetriever(NamingStrategy strategy) 
            => _strategy = strategy;

        public override string? GetName(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(false);
            var argument = attributes.FirstOrDefault(attr => attr is GraphqlArgumentAttribute);

            if (argument is not GraphqlArgumentAttribute propertyNameAttr) 
                return base.GetName(propertyInfo);
            
            var propertyName = string.IsNullOrEmpty(propertyNameAttr.PropertyName) 
                ? _strategy.GetName(propertyInfo)
                : propertyNameAttr.PropertyName;
                
            return $"{propertyName}()";
        }
    }
}