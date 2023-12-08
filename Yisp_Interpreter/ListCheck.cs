using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class ListCheck : ICallable
    {
        public Range Arity()
        {
            return new Range(1, 1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object obj = interpreter.Evaluate(arguments[0]);

            if (obj == null || obj is List<object> || obj is SExpr.List)
            {
                return true;
            }
            else
            {
                return null;
            }
        }
    }
}
