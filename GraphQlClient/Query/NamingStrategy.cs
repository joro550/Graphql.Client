using System.Reflection;

namespace GraphQlClient.Query
{
    public abstract class NamingStrategy
    {
        public abstract string GetName(PropertyInfo prop);
    }
}