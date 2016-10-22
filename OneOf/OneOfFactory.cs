using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    internal static class OneOfFactory<TOneOf>
        where TOneOf : IOneOf
    {
        /// <summary>The OneOf's type</summary>
        static readonly TypeInfo oneofTypeInfo = typeof(TOneOf).GetTypeInfo();

        /// <summary>The OneOf's value types it supports (plus subclasses)</summary>
        static readonly TypeInfo[] oneofArgTypeInfos = oneofTypeInfo.GenericTypeArguments.Select(x => x.GetTypeInfo()).ToArray();

        /// <summary>Function to quickly create instances of OneOf without needing reflection</summary>
        static readonly Func<object, Type, TOneOf> createInstance = GetCreateInstanceFunc();

        /// <summary>Maps value type to one of the OneOf generic arguments</summary>
        static readonly Dictionary<TypeInfo, TypeInfo> mapValueTypeToGenericArgType
            = oneofArgTypeInfos.ToDictionary(k=>k, v=>v);

        /// <summary>
        /// Create an instance of OneOf
        /// </summary>
        public static TOneOf Create(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueTypeInfo = value.GetType().GetTypeInfo();

            var matchingTypeInfo = GetBestMatchingType(valueTypeInfo);

            if (matchingTypeInfo == null)
            {
                var genArgs = string.Join(", ", oneofArgTypeInfos.Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueTypeInfo.Name} is not compatible with OneOf<{genArgs}>", nameof(value));
            }

            var oneofInstance = createInstance(value, matchingTypeInfo.AsType());

            return oneofInstance;
        }

        /// <summary>
        /// Get the OneOf's Tn the value type best matches, or null.
        /// </summary>
        static TypeInfo GetBestMatchingType(TypeInfo valueTypeInfo)
        {
            TypeInfo bestTypeInfo = null;

            if (mapValueTypeToGenericArgType.TryGetValue(valueTypeInfo, out bestTypeInfo))
                return bestTypeInfo;

            foreach (var argTypeInfo in oneofArgTypeInfos)
            {
                // is this OneOf Generic Parameter a match for the value?
                if (argTypeInfo.IsAssignableFrom(valueTypeInfo))
                {
                    // is this OneOf Generic Parameter a better match than what we've seen previously.
                    if (bestTypeInfo == null ||
                        bestTypeInfo.IsAssignableFrom(argTypeInfo) && !argTypeInfo.IsAssignableFrom(bestTypeInfo))
                    {
                        bestTypeInfo = argTypeInfo;
                    }
                }
            }

            mapValueTypeToGenericArgType.Add(valueTypeInfo, bestTypeInfo);

            return bestTypeInfo;
        }

        /// <summary>
        /// Create a Func that quickly creates an instance of the OneOf.
        /// </summary>
        static Func<object, Type, TOneOf> GetCreateInstanceFunc()
        {
            var oneofCtor = oneofTypeInfo.DeclaredConstructors.First();

            var parmValueExpr = Expression.Parameter(typeof(object), "value");
            var parmTypeExpr = Expression.Parameter(typeof(Type), "type");
            var newExpr = Expression.New(oneofCtor, parmValueExpr, parmTypeExpr);
            var lambdaExpr = Expression.Lambda<Func<object, Type, TOneOf>>(newExpr, parmValueExpr, parmTypeExpr);
            var func = lambdaExpr.Compile();
            return func;
        }
    }
}
