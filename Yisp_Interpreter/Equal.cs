using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Equal : ICallable
    {
        public Range Arity()
        {
            return new Range(2, 2);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object[] o = new object[2];

            for (int i = 0; i < 2; i++)
            {
                object arg = interpreter.Evaluate(arguments[i]);
                if (arg is List<object>)
                {
                    return null;
                }
                o[i] = arg;
            }

            return interpreter.IsEqual(o[0], o[1]) ? true : null;
        }
    }
}
