using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public abstract class SExpr
    {
        public abstract T Accept<T>(IVisitor<T> visitor);

        public interface IVisitor<T>
        {
            public T VisitAtomSExpr(Atom expr);
            public T VisitListSExpr(List expr);
        }

        public class Atom : SExpr
        {
            public readonly object Value;

            public Atom(object value)
            {
                Value = value;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitAtomSExpr(this);
            }

            public override string ToString()
            {
                if (Value is string s)
                {
                    return $"\"{s}\"";
                }
                else if (Value is Token t)
                {
                    return t.Lexeme;
                }
                else
                {
                    return Value.ToString();
                }
            }
        }

        public class List : SExpr
        {
            public readonly List<SExpr> Values;

            public List(List<SExpr> values)
            {
                Values = values;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitListSExpr(this);
            }

            public override string ToString()
            {
                string output = "(";

                // Traverse list elements
                for (int i = 0; i < Values.Count; i++)
                {
                    output += Values[i].ToString();

                    // Print separator
                    if (i != Values.Count - 1)
                    {
                        output += " ";
                    }
                }

                output += ")";
                return output;
            }
        }
    }
}
