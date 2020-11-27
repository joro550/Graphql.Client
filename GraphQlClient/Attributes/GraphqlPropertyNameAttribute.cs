using System;

namespace GraphQlClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphqlPropertyNameAttribute : Attribute
    {
        public string PropertyName { get; }

        public GraphqlPropertyNameAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

    }
}