﻿using System.Linq;
using System.Reflection;
using GraphQlClient.Attributes;

namespace GraphQlClient.Query.NameRetriever
{
    public class AttributeNameRetriever : AbstractNameRetriever
    {
        public override string? GetName(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(false);
            var firstOrDefault = attributes.FirstOrDefault(attr => attr is GraphqlPropertyNameAttribute);
            if (firstOrDefault is GraphqlPropertyNameAttribute propertyNameAttr)
            {
                return propertyNameAttr.PropertyName;
            }

            return base.GetName(propertyInfo);
        }
    }
}