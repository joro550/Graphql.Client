using System.Reflection;

namespace GraphQlClient.Query
{
    public abstract class NamingStrategy
    {
        public abstract string Name(PropertyInfo prop);
    }
}