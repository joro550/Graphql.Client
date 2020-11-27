using System.Reflection;

namespace GraphQlClient.Query.NameRetriever
{
    public class AbstractNameRetriever
    {
        private AbstractNameRetriever? _nextHandler;

        public AbstractNameRetriever SetNext(AbstractNameRetriever handler)
        {
            _nextHandler = handler;
            return handler;
        }
        
        public virtual string? GetName(PropertyInfo propertyInfo) 
            => _nextHandler?.GetName(propertyInfo);
    }
}