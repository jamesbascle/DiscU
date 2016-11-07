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
        static readonly TypeInfo oneOfType = typeof(TOneOf).GetTypeInfo();

        /// <summary>The OneOf's permitted value types</summary>
        static readonly TypeInfo[] oneOfPermittedValueTypes = oneOfType.GenericTypeArguments.Select(x => x.GetTypeInfo()).ToArray();

        /// <summary>Function to quickly create instances of OneOf without needing reflection</summary>
        static readonly Func<object, Type, TOneOf> createOneOfInstance = GetCreateInstanceFunc();
            
        /// <summary>Maps value type to one of the OneOf's permitted value types</summary>
        static readonly Dictionary<TypeInfo, TypeInfo> mapValueTypeToOneOfPermittedType
                                   = oneOfPermittedValueTypes.ToDictionary(k => k);


        /// <summary>
        /// Create an instance of OneOf
        /// </summary>
        public static TOneOf Create(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType().GetTypeInfo();

            var matchingType = GetBestMatchingType(valueType);

            if (matchingType == null)
            {
                var genArgs = string.Join(", ", oneOfPermittedValueTypes.Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{genArgs}>", nameof(value));
            }

            var oneofInstance = createOneOfInstance(value, matchingType.AsType());

            return oneofInstance;
        }

        /// <summary>
        /// <summary>
        /// Get the OneOf's Tn that best matches the value, or null.
        /// </summary>
        static TypeInfo GetBestMatchingType(TypeInfo valueType)
        {
            TypeInfo bestType = null;

            if (mapValueTypeToOneOfPermittedType.TryGetValue(valueType, out bestType))
                return bestType;

            foreach (var permittedType in oneOfPermittedValueTypes)
            {
                // does the value match this Tn?
                if (permittedType.IsAssignableFrom(valueType))
                {
                    // is this Tn a better match than seen previously?
                    if (bestType == null ||
                        bestType.IsAssignableFrom(permittedType) && !permittedType.IsAssignableFrom(bestType))
                    {
                        bestType = permittedType;
                    }
                }
            }

            mapValueTypeToOneOfPermittedType.Add(valueType, bestType);

            return bestType;
        }

        /// <summary>
        /// Create a Func that quickly creates an instance of the OneOf.
        /// </summary>
        static Func<object, Type, TOneOf> GetCreateInstanceFunc()
        {
            var oneofCtor = oneOfType.DeclaredConstructors.First();

            var parmValueExpr = Expression.Parameter(typeof(object), "value");
            var parmTypeExpr = Expression.Parameter(typeof(Type), "type");
            var newExpr = Expression.New(oneofCtor, parmValueExpr, parmTypeExpr);
            var lambdaExpr = Expression.Lambda<Func<object, Type, TOneOf>>(newExpr, parmValueExpr, parmTypeExpr);
            var func = lambdaExpr.Compile();
            return func;
        }
    }
}
