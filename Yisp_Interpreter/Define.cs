﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Define : ICallable
    {
        public Range Arity()
        {
            return new Range(3, 3);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            if (!new StackFrame(4).GetMethod().Name.Equals("Interpret"))
            {
                throw new RuntimeException("Cannot define a function in nested code.");
            }

            if (arguments[0] is SExpr.Atom a && a.Value is Token t && t.Type == TokenType.Symbol)
            {
                if (Interpreter.NativeFunctions.ContainsKey(t.Lexeme))
                {
                    throw new RuntimeException("Cannot overwrite built-in functions.");
                }

                if (arguments[1] == null)
                {
                    interpreter.Environment.Define(t.Lexeme, new Function(new List<Token>(), arguments[2]));

                    throw new StatementException();
                }
                else if (arguments[1] is SExpr.List l)
                {
                    List<Token> args = new();
                    foreach (SExpr s in l.Values)
                    {
                        if (s is SExpr.Atom aa && aa.Value is Token tt && tt.Type == TokenType.Symbol)
                        {
                            args.Add(tt);
                        }
                        else
                        {
                            throw new RuntimeException("Name of function argument must be a symbol.");
                        }
                    }
                    interpreter.Environment.Define(t.Lexeme, new Function(args, arguments[2]));

                    throw new StatementException();
                }
                else
                {
                    throw new RuntimeException("Expected list of function arguments.");
                }
            }
            else
            {
                throw new RuntimeException("Name of function must be a symbol.");
            }
        }
    }
}
