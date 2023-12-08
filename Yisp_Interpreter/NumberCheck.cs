using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class NumberCheck : ICallable
    {
        public Range Arity()
        {
            return new Range(1, 1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object obj = interpreter.Evaluate(arguments[0]);
            return obj is double || (obj is SExpr.Atom a && a.Value is double) ? true : null;
        }
    }
}
