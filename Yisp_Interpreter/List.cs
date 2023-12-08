using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class List : ICallable
    {
        public Range Arity()
        {
            return new Range(0, -1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            List<object> result = new();
            foreach (SExpr s in arguments)
            {
                result.Add(interpreter.Evaluate(s));
            }
            if (result.Count != 0)
            {
                result.Add(null);
            }

            return result.Count == 0 ? null : result;
        }
    }
}
