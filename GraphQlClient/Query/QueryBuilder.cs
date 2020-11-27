﻿using System;
using System.Text;
using System.Reflection;
using GraphQlClient.Query.NameRetriever;

namespace GraphQlClient.Query
{
    internal class QueryBuilder
    {
        private readonly AbstractNameRetriever _nameRetriever;
        
        public QueryBuilder(NamingStrategy strategy) 
            => _nameRetriever = CreateChain(strategy);

        private static AttributeNameRetriever CreateChain(NamingStrategy strategy)
        {
            var attributeNameRetriever = new AttributeNameRetriever();
            var propertyNameRetriever = new PropertyNameRetriever(strategy);

            attributeNameRetriever
                .SetNext(propertyNameRetriever);
            return attributeNameRetriever;
        }

        public string CreateRetrieveQuery<T>(T query) =>
            new StringBuilder()
                .Append('{')
                .Append(AddProperties(query))
                .Append('}')
                .ToString();

        private string AddProperties<T>(T thing)
        {
            var sb = new StringBuilder();
            var properties = thing?.GetType().GetProperties() ?? Array.Empty<PropertyInfo>();
            
            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(thing, null);

                var type = prop.PropertyType;
                var name = _nameRetriever.GetName(prop);
                if(string.IsNullOrEmpty(name))
                    continue;
                
                if (type.IsValueType)
                {
                    sb.Append($"{name},");
                }
                else if (type == typeof(string))
                {
                    sb.Append($"{name},");
                }
                else if (type.IsClass)
                {
                    var typedPropertyVal = Convert.ChangeType(propValue, prop.PropertyType);
                    sb.Append($"{name} {{{AddProperties(typedPropertyVal)}}}");
                }
            }
            
            // If the last character is a comma (,) then remove it
            if(sb[^1] == ',')
                return sb
                    .ToString(0, sb.Length - 1);

            // return the full string
            return sb
                .ToString();
        }
    }
}