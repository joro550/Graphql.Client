using System;

namespace GraphQlClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphqlArgumentAttribute : Attribute
    {
        public string? PropertyName { get; set; }
        public string? ArgumentName { get; set; }
    }
    
    
    [AttributeUsage(AttributeTargets.Class)]
    public class GraphqlFunctionAttribute : Attribute
    {
        private string FunctionName { get; }

        public GraphqlFunctionAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}