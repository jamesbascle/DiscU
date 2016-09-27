using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneOf
{
    public interface IMatchResultGiver<TResult>
    {
        TResult Result { get; }
    }

    public interface IOtherwiser<TResult> : IMatchResultGiver<TResult>
    {
        IMatchResultGiver<TResult> Otherwise(Func<object, TResult> func);
        IMatchResultGiver<TResult> OtherwiseThrow(Func<object, Exception> func);
    }
}
