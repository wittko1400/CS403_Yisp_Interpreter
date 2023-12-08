using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class NilCheck : ICallable
    {
        public Range Arity()
        {
            return new Range(1, 1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object obj = interpreter.Evaluate(arguments[0]);
            return obj == null || (obj is SExpr.List l && l.Values.Count == 0) ? true : null;
        }
    }
}
