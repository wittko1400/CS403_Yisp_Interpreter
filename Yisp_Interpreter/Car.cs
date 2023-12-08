using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Car : ICallable
    {
        public Range Arity()
        {
            return new Range(1, 1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object obj = interpreter.Evaluate(arguments[0]);

            if (obj is List<object> carList)
            {
                return carList[0];
            }
            else if (obj is SExpr.List sl && sl.Values.Count != 0)
            {
                object o = sl.Values[0];
                if (o is SExpr.List sl2 && sl2.Values.Count == 0)
                {
                    return null;
                }
                else if (o is SExpr.Atom a)
                {
                    if (a.Value is double d)
                    {
                        return d;
                    }
                    else if (a.Value is string s)
                    {
                        return s;
                    }
                    else
                    {
                        return o;
                    }
                }
                else
                {
                    return o;
                }
            }
            else
            {
                throw new RuntimeException($"Operand must be a list.");
            }
        }
    }
}
