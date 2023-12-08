using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Cons : ICallable
    {
        public Range Arity()
        {
            return new Range(2, 2);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object left = interpreter.Evaluate(arguments[0]);
            object right = interpreter.Evaluate(arguments[1]);

            if (right is List<object> consList)
            {
                return consList.Prepend(left).ToList();
            }
            else
            {
                return new List<object>() { left, right };
            }
        }
    }
}
