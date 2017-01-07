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

        /// <summary>Cache the list of Tns for the OneOf as calling GetGenericArguments is SLOW.</summary>
        static readonly Type[] OneOfTns = typeof(TOneOf).GetGenericArguments();

        /// <summary>Ctor</summary>
        protected OneOfBase(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType();
            var valueTn = MatchExactTn(valueType) ?? MatchClosestTn(valueType);

            if (valueTn == null)
            {
                var oneOfTnCsv = string.Join(", ", OneOfBase<TOneOf>.OneOfTns.Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{oneOfTnCsv}>", nameof(value));
            }

            this.Value = value;
            this.ValueTn = valueTn;
        }

        /// <summary>
        /// Find the exact matching Tn, or null.
        /// </summary>
        static Type MatchExactTn(Type valueType)
        {
            // it's nearly 2x faster if we use a local variable than access the static OneOfTns repeatedly.
            var oneOfTns = OneOfBase<TOneOf>.OneOfTns;

            switch (oneOfTns.Length)
            {
                case 9:
                    if (oneOfTns[8] == valueType) return valueType;
                    goto case 8;
                case 8:
                    if (oneOfTns[7] == valueType) return valueType;
                    goto case 7;
                case 7:
                    if (oneOfTns[6] == valueType) return valueType;
                    goto case 6;
                case 6:
                    if (oneOfTns[5] == valueType) return valueType;
                    goto case 5;
                case 5:
                    if (oneOfTns[4] == valueType) return valueType;
                    goto case 4;
                case 4:
                    if (oneOfTns[3] == valueType) return valueType;
                    goto case 3;
                case 3:
                    if (oneOfTns[2] == valueType) return valueType;
                    goto case 2;
                case 2:
                    if (oneOfTns[1] == valueType) return valueType;
                    goto case 1;
                case 1:
                    if (oneOfTns[0] == valueType) return valueType;
                    break;
            }

            return null;
        }

        /// <summary>
        /// find the best matching Tn that value is a subclass of, otherwise null.
        /// </summary>
        static Type MatchClosestTn(Type valueType)
        {
            Type bestType = null;

            foreach (var tn in OneOfBase<TOneOf>.OneOfTns)
            {
                // does the value match this Tn?
                if (tn.IsAssignableFrom(valueType))
                {
                    // is this Tn a better match than seen previously?
                    if (bestType == null ||
                        bestType.IsAssignableFrom(tn) && !tn.IsAssignableFrom(bestType))
                    {
                        bestType = tn;
                    }
                }
            }

            return bestType;
        }

    }
}
