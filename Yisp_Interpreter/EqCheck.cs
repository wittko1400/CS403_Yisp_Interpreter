using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class EqCheck : ICallable
    {
        public Range Arity()
        {
            return new Range(2, 2);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            object o1, o2;
            o1 = interpreter.Evaluate(arguments[0]);
            o2 = interpreter.Evaluate(arguments[1]);

            if (o1 is SExpr.Atom a1 && o2 is SExpr.Atom a2 &&
                a1.Value is Token t1 && a2.Value is Token t2 &&
                t1.Type == TokenType.Symbol && t2.Type == TokenType.Symbol &&
                t1.Lexeme.Equals(t2.Lexeme))
            {
                return true;
            }
            else if (ReferenceEquals(o1, o2))
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
