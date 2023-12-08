using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Add : ICallable
    {
        public Range Arity()
        {
            return new Range(2, -1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            double result = 0;
            foreach (SExpr s in arguments)
            {
                object arg = interpreter.Evaluate(s);
                if (arg is not double d)
                {
                    throw new RuntimeException($"Operand '{interpreter.Stringify(arg)}' is not a number.");
                }
                result += d;
            }
            return result;
        }
    }
}
