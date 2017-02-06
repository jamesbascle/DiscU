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
        protected readonly object value;

        /// <summary>The matching OneOf's Tn type that best matches the value.</summary>
        protected readonly Type valueTn;

        /// <summary>Cache the list of Tns for the OneOf as calling GetGenericArguments is SLOW.</summary>
        static readonly Type[] oneOfTns = typeof(TOneOf).GetGenericArguments();

        /// <summary>The OneOf's Tns formatted as comma-separated type names for error reporting</summary>
        static readonly string oneOfTnsCsv = string.Join(", ", oneOfTns.Select(type => type.Name));

        /// <summary>Cache of best matches between OneOf value type and the OneOf Tn's.</summary>
        static readonly ConcurrentDictionary<Type,Type> valueTypeToTnCache = new ConcurrentDictionary<Type, Type>();
        
        /// <summary>Ctor</summary>
        /// <param name="value">The OneOf's value</param>
        /// <param name="matchedType">The OneOf's Tn that best matches the value if known, otherwise null.</param>
        protected OneOfBase(object value, Type matchedType)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var valueType = value.GetType();
            var valueTn = (valueType == matchedType) ? valueType : GetBestType(valueType);

            if (valueTn == null )
                throw new ArgumentException($"Value of type {valueType.Name} is not compatible with OneOf<{oneOfTnsCsv}>", nameof(value));

            this.value = value;
            this.valueTn = valueTn;
        }

        /// <summary>
        /// find the best matching Tn for valueType, otherwise null.
        /// </summary>
        static Type GetBestType(Type valueType)
        {
            Type bestType = null;

            // OPTIMISATION
            // Using TryGetValue with GetOrAdd is 6x faster than GetOrAdd alone(!).

            if (!valueTypeToTnCache.TryGetValue(valueType, out bestType))      
                bestType = valueTypeToTnCache.GetOrAdd(valueType, GetBestTypeUncached);

            return bestType;
        }

        /// <summary>
        /// find the best matching Tn for valueType, otherwise null.
        /// </summary>
        static Type GetBestTypeUncached(Type valueType)
        {
            Type bestType = null;

            foreach (var tn in oneOfTns)
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
