using System;

namespace GraphQlClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphqlArgumentParameterAttribute  : Attribute
    {
        public string ArgumentName { get; set; }
            = string.Empty;
    }
}