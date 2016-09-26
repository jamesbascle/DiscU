using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    public struct OneOfMatcher<T>
    {
        object oneOfValue;
        object result;

        public OneOfMatcher(object oneOfValue)
        {
            this.oneOfValue = oneOfValue;
            this.result = null;
        }

        public OneOfMatcher<T> When<TValue>(Func<TValue, T> func)
        {
            if (oneOfValue.GetType() == typeof(TValue))
                result = func.Invoke((TValue)oneOfValue);

            return this;
        }

        public OneOfMatcher<T> Otherwise(Func<object, T> func)
        {
            result = func.Invoke(oneOfValue);
            return this;
        }

        public T Result
        {
            get
            {
                if (result == null) throw new InvalidOperationException("No match found");
                return (T)result;
            }
        }
    }
}
