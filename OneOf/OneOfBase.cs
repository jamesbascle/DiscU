using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    public abstract class OneOfBase<TOneOf>
    {
        /// <summary>The OneOf's value</summary>
        internal readonly object Value;

        /// <summary>The matching OneOf's Tn type that the value is, or is derived from.</summary>
        internal readonly Type ValueTn;

        /// <summary>Function to quickly determine if type equals Tn</summary>
        static readonly Func<Type, Boolean> equalsTn = GetEqualsTnFunc();

        /// <summary>Ctor</summary>
        protected OneOfBase(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType();
            var matchingType = GetBestMatchingTn(valueType);

            if (matchingType == null)
            {
                var oneOfTnCsv = string.Join(", ", typeof(TOneOf).GetGenericArguments().Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{oneOfTnCsv}>", nameof(value));
            }

            this.Value = value;
            this.ValueTn = matchingType;
        }

        /// <summary>
        /// Get the OneOf's Tn that best matches the value, or null.
        /// </summary>
        static Type GetBestMatchingTn(Type valueType)
        {
            Type bestType = null;

            // fastest case: the type equals Tn?

            if (equalsTn(valueType))
                return valueType;

            // slowest case: find the best matching Tn, if subclass

            foreach (var permittedType in typeof(TOneOf).GetGenericArguments())
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
        /// Create a Func that quickly determines if the ValueType equals Tn
        /// </summary>
        static Func<Type, Boolean> GetEqualsTnFunc()
        {
            var typeParm = Expression.Parameter(typeof(Type), "type");
            var labelReturn = Expression.Label(typeof(Boolean));
            var body = new List<Expression>();

            foreach (var tn in typeof(TOneOf).GetGenericArguments())
            {
                // return true if typeInfo == Tn
                var expr = Expression.IfThen(Expression.Equal(typeParm, Expression.Constant(tn)), Expression.Return(labelReturn, Expression.Constant(true)));
                body.Add(expr);
            }

            // otherwise default is false
            body.Add(Expression.Label(labelReturn, Expression.Constant(false)));

            var lambdaExpr = Expression.Lambda<Func<Type, Boolean>>(Expression.Block(body.ToArray()), typeParm);
            var func = lambdaExpr.Compile();
            return func;
        }
    }
}
