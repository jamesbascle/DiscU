using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    public interface IOneOfMatcherTerminator<TResult>
    {
        TResult Else(TResult defaultValue);
        TResult Else(Func<object, TResult> createResult);
        TResult ElseThrow(Func<object, Exception> createException);
    }
}
