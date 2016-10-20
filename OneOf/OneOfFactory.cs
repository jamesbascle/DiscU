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
        where TOneOf: IOneOf
    {
        static readonly TypeInfo oneofTypeInfo = typeof(TOneOf).GetTypeInfo();
        static readonly TypeInfo[] oneofArgTypeInfos = oneofTypeInfo.GenericTypeArguments.Select(x => x.GetTypeInfo()).ToArray();
        static readonly ConstructorInfo oneofCtor = oneofTypeInfo.DeclaredConstructors.First();
        static readonly Func<object, Type, TOneOf> createInstance = GetCreateInstanceFunc();

        /// <summary>
        /// Maps value type to one of the OneOf generic arguments
        /// </summary>
        static readonly Dictionary<Type, Type> mapValueTypeToGenericArgType
            = new Dictionary<Type, Type>(10);

        internal static TOneOf Create(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType();

            if (oneofArgTypeInfos.Contains(valueType.GetTypeInfo()))
                return createInstance(value, valueType);

            Type bestType;

            if (!mapValueTypeToGenericArgType.TryGetValue(valueType, out bestType))
            {
                bestType = GetBestType(valueType);

                if (bestType == null)
                {
                    var genArgs = string.Join(", ", oneofArgTypeInfos.Select(type => type.Name));
                    throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{genArgs}>", nameof(value));
                }

                mapValueTypeToGenericArgType.Add(valueType, bestType);
            }

            var oneofInstance = createInstance(value, bestType);

            return oneofInstance;
        }

        /// <summary>
        /// Which OneOf generic argument is the best match for valueType?
        /// </summary>
        static Type GetBestType(Type valueType)
        {
            var valueTypeInfo = valueType.GetTypeInfo();
            TypeInfo bestTypeInfo = null;

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

            if (bestTypeInfo == null)
                return null;

            return bestTypeInfo.AsType();
        }

        static Func<object, Type, TOneOf> GetCreateInstanceFunc()
        {
            var parmValueExpr = Expression.Parameter(typeof(object), "value");
            var parmTypeExpr = Expression.Parameter(typeof(Type), "type");
            var newExpr = Expression.New(oneofCtor, parmValueExpr, parmTypeExpr);
            var lambdaExpr = Expression.Lambda<Func<object, Type, TOneOf>>(newExpr, parmValueExpr, parmTypeExpr);
            var func = lambdaExpr.Compile();
            return func;
        }
    }
}
