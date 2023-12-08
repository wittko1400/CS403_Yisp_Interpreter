using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Interpreter : SExpr.IVisitor<object>
    {
        public readonly Environment Globals = new();
        public Environment Environment;

        public static readonly Dictionary<string, ICallable> NativeFunctions = new()
        {
            { "+", new Add() },
            { "-", new Sub() },
            { "*", new Multiply() },
            { "/", new Division() },
            { "=", new Equal() },
            { ">", new Greater() },
            { "<", new Lesser() },
            { "list", new List() },
            { "cons", new Cons() },
            { "car", new Car() },
            { "cdr", new Cdr() },
            { "and?", new And() },
            { "or?", new Or() },
            { "not?", new NotCheck() },
            { "list?", new ListCheck() },
            { "number?", new NumberCheck() },
            { "symbol?", new SymbolP() },
            { "nil?", new NilCheck() },
            { "eq?", new EqCheck() },
            { "cond", new Conditional() },
            { "set", new Set() },
            { "define", new Define() },
            { "quote", new Quote() },
            { "eval", new Eval() },
        };

        public Interpreter()
        {
            Environment = Globals;
            foreach (KeyValuePair<string, ICallable> kvp in NativeFunctions)
            {
                Globals.Define(kvp.Key, kvp.Value);
            }
        }

        public void Interpret(List<SExpr> expressions)
        {
            try
            {
                foreach (SExpr expr in expressions)
                {
                    try
                    {
                        Console.WriteLine(Stringify(Evaluate(expr)));
                    }
                    catch (StatementException) { }
                }
            }
            catch (RuntimeException e)
            {
                Yisp.RuntimeError(e);
            }
        }


        public string Stringify(object obj)
        {
            if (obj == null)
            {
                return "()";
            }
            else if (obj is List<object> l)
            {
                return StringifyList(l);
            }
            else if (obj is bool b)
            {
                return b ? "T" : "()";
            }
            else if (obj is string s)
            {
                return $"\"{s}\"";
            }
            else
            {
                return obj.ToString();
            }
        }


        public string StringifyList(List<object> l)
        {
            string output = "(";
            bool lastIsNil = (l.Last() == null);

            for (int i = 0; i < l.Count; i++)
            {
                bool isLast = (i == l.Count - 1);
                if (isLast)
                {
                    output += ". ";
                }

                output += Stringify(l[i]);

                if (lastIsNil && i == l.Count - 2)
                {
                    break;
                }
                else if (!isLast)
                {
                    output += " ";
                }
            }
            output += ")";
            return output;
        }


        public bool IsEqual(object a, object b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null)
            {
                return false;
            }
            else if (a is SExpr.Atom atomA && b is SExpr.Atom atomB)
            {
                return atomA.Value.Equals(atomB.Value);
            }
            else
            {
                return a.Equals(b);
            }
        }

        public bool IsTruthy(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is bool b)
            {
                return b;
            }
            else
            {
                return true;
            }
        }


        public object Evaluate(SExpr expr)
        {
            return expr.Accept(this);
        }

        public object VisitAtomSExpr(SExpr.Atom expr)
        {
            if (expr.Value is Token t && (t.Type == TokenType.Symbol || Parser.Operations.Contains(t.Type)))
            {
                return Environment.Get(t);
            }
            else
            {
                return expr.Value;
            }
        }

        public object VisitListSExpr(SExpr.List expr)
        {
            if (expr.Values.Count == 0)
            {
                return null;
            }
            else
            {
                object firstElement = Evaluate(expr.Values[0]);

                if (firstElement is ICallable f)
                {
                    if (f.Arity().IsInRange(expr.Values.Count - 1))
                    {
                        return f.Call(this, expr.Values.Skip(1).ToList());
                    }
                    else
                    {
                        throw new RuntimeException($"Incorrect number of arguments for function. Expected {f.Arity()} argument(s), got {expr.Values.Count - 1}.");
                    }
                }
                else
                {
                    throw new RuntimeException($"Cannot call non-symbol value '{Stringify(firstElement)}'.");
                }
            }
        }
    }
}
