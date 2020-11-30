using System;

namespace GraphQlClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphqlArgumentAttribute : Attribute
    {
        public string? PropertyName { get; set; }
        public string? ArgumentName { get; set; }
    }
}