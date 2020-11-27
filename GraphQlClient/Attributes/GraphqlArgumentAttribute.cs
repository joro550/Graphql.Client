using System;

namespace GraphQlClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphqlArgumentAttribute : Attribute
    {
        public string? ArgumentName { get; set; }
    }
}