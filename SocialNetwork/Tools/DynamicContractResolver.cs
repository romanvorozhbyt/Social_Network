using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SocialNetwork.Tools
{
    public class DynamicContractResolver<T> : DefaultContractResolver
    {
        private readonly HashSet<string> _propertiesToExclude = new HashSet<string>();

        public DynamicContractResolver(params string[] prop)
        {
            foreach (var propertyName in prop)
                _propertiesToExclude.Add(propertyName);
        }

        public DynamicContractResolver(params Expression<Func<T, object>>[] propertyExpressions)
        {
            foreach (Expression<Func<T, object>> expression in propertyExpressions)
                _propertiesToExclude.Add(expression.GetReturnedPropertyName());
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> jsonProperties =
                base.CreateProperties(type, memberSerialization);

            //if (typeof(T).IsAssignableFrom(type))
            //{
            jsonProperties = jsonProperties
                .Where(pr => !this._propertiesToExclude.Contains(pr.PropertyName))
                .ToList();
            //}

            return jsonProperties;
        }
    }

    public static class ExtensionForExpression
    {
        public static string GetReturnedPropertyName<T, TR>(this Expression<Func<T, TR>> propertyLambda)
        {
            var member = propertyLambda.Body as MemberExpression;
            var memberPropertyInfo = member?.Member as PropertyInfo;
            return memberPropertyInfo?.Name;
        }

    }
}