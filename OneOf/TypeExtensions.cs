using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OneOf
{
    internal static class TypeExtensions
    {
        internal static Type[] GetGenericArguments(this Type type)
        {
#if NET40
            return type.GetGenericArguments();
#else
            return type.GetTypeInfo().GenericTypeArguments;
#endif
        }

        internal static bool IsAssignableFrom(this Type type, Type otherType)
        {
#if NET40
            return type.IsAssignableFrom(otherType);
#else
            return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
#endif
        }
    }
}
