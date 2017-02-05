using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OneOf
{
    public abstract class OneOfBase<TOneOf>  where TOneOf : OneOfBase<TOneOf>
    {
        /// <summary>The OneOf's value</summary>
        internal readonly object Value;

        /// <summary>The matching OneOf's Tn type that the value is, or is derived from.</summary>
        internal readonly Type ValueTn;

        /// <summary>Cache the list of Tns for the OneOf as calling GetGenericArguments is SLOW.</summary>
        static readonly Type[] OneOfTns = typeof(TOneOf).GetGenericArguments();

        private static readonly ConcurrentDictionary<Type,Type> TypeToTypeCache = new ConcurrentDictionary<Type, Type>();
        
        /// <summary>Ctor</summary>
        protected OneOfBase(object value, Type matchedType)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType();
            var valueTn = matchedType == valueType ? valueType : GetBestType(valueType);
            //var valueTn = GetBestType(valueType);

            if (valueTn == null )
            {
                var oneOfTnCsv = string.Join(", ", OneOfBase<TOneOf>.OneOfTns.Select(type => type.Name));
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{oneOfTnCsv}>", nameof(value));
            }

            this.Value = value;
            this.ValueTn = valueTn;
        }

        private static Type GetBestType(Type valueType)
        {
            Type ret;
            if (!TypeToTypeCache.TryGetValue(valueType, out ret))
            {
                ret = TypeToTypeCache.GetOrAdd(valueType, MatchClosestTn(valueType));
            }
            return ret;
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
                    if (bestType == null || bestType.IsAssignableFrom(tn) && !tn.IsAssignableFrom(bestType))
                    {
                        bestType = tn;
                    }
                }
            }

            return bestType;
        }

    }
}
