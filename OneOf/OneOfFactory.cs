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

        /// <summary>The OneOf's Tn types</summary>
        static readonly TypeInfo[] oneOfTnTypes = oneOfType.GenericTypeArguments.Select(x => x.GetTypeInfo()).ToArray();

        /// <summary>Function to quickly create instances of OneOf without needing reflection</summary>
        static readonly Func<object, Type, TOneOf> createOneOfInstance = GetCreateInstanceFunc();

        /// <summary>Function to quickly determine if type equals Tn</summary>
        static readonly Func<TypeInfo, Boolean> equalsTn = GetEqualsTnFunc();

        /// <summary>
        /// Create an instance of OneOf
        /// </summary>
        public static TOneOf Create(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType().GetTypeInfo();

            var matchingType = GetBestMatchingTn(valueType);

            if (matchingType == null)
            {
                var oneOfTnCsv = string.Join(", ", oneOfTnTypes.Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{oneOfTnCsv}>", nameof(value));
            }

            var oneofInstance = createOneOfInstance(value, matchingType.AsType());

            return oneofInstance;
        }

        /// <summary>
        /// Get the OneOf's Tn that best matches the value, or null.
        /// </summary>
        static TypeInfo GetBestMatchingTn(TypeInfo valueType)
        {
            TypeInfo bestType = null;

            // fastest case: the type equals Tn?

            if (equalsTn(valueType))
                return valueType;

            // slowest case: find the best matching Tn, if subclass

            foreach (var permittedType in oneOfTnTypes)
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

        /// <summary>
        /// Create a Func that quickly determines if the ValueType equals Tn
        /// </summary>
        static Func<TypeInfo, Boolean> GetEqualsTnFunc()
        {
            var typeInfoParm = Expression.Parameter(typeof(Type), "typeInfo");
            var labelReturn = Expression.Label(typeof(Boolean));
            var body = new List<Expression>();

            foreach(var tn in oneOfTnTypes)
            {
                // return true if typeInfo == Tn
                var expr = Expression.IfThen(Expression.Equal(typeInfoParm, Expression.Constant(tn)), Expression.Return(labelReturn, Expression.Constant(true)));
                body.Add(expr);
            }

            // otherwise default is false
            body.Add(Expression.Label(labelReturn, Expression.Constant(false)));

            var lambdaExpr = Expression.Lambda<Func<TypeInfo, Boolean>>(Expression.Block(body.ToArray()), typeInfoParm);
            var func = lambdaExpr.Compile();
            return func;
        }
    }
}
