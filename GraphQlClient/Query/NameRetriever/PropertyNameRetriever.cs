using System.Reflection;

namespace GraphQlClient.Query.NameRetriever
{
    public class PropertyNameRetriever : AbstractNameRetriever
    {
        private readonly NamingStrategy _namingStrategy;

        public PropertyNameRetriever(NamingStrategy namingStrategy) 
        {
            _namingStrategy = namingStrategy;
        }

        public override string? GetName(PropertyInfo propertyInfo) 
            => _namingStrategy.GetName(propertyInfo);
    }
}