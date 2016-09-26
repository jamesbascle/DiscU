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
        bool switched;

        public OneOfSwitcher(object oneOfValue)
        {
            this.oneOfValue = oneOfValue;
            this.switched = false;
        }

        public OneOfSwitcher When<TValue>(Action<TValue> action)
        {
            if (oneOfValue is TValue)
            {
                action.Invoke((TValue)oneOfValue);
                switched = true;
            }

            return this;
        }

        public void Otherwise(Action<object> action)
        {
            if (!switched)
            {
                action.Invoke(oneOfValue);
            }
        }
    }
}
