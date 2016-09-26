using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    public struct OneOfSwitcher
    {
        object oneOfValue;

        public OneOfSwitcher(object oneOfValue)
        {
            this.oneOfValue = oneOfValue;
        }

        public OneOfSwitcher When<TValue>(Action<TValue> action)
        {
            if (oneOfValue.GetType() == typeof(TValue))
                action.Invoke((TValue)oneOfValue);

            return this;
        }

        public void Otherwise(Action<object> action)
        {
            action.Invoke(oneOfValue);
        }
    }
}
