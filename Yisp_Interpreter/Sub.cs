﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Sub : ICallable
    {
        public Range Arity()
        {
            return new Range(1, -1);
        }

        public object Call(Interpreter interpreter, List<SExpr> arguments)
        {
            double result = 0;
            for (int i = 0; i < arguments.Count; i++)
            {
                object arg = interpreter.Evaluate(arguments[i]);
                if (arg is not double d)
                {
                    throw new RuntimeException($"Operand '{interpreter.Stringify(arg)}' is not a number.");
                }

                if (i == 0)
                {
                    result = arguments.Count == 1 ? -d : d;
                }
                else
                {
                    result -= d;
                }
            }

            return result;
        }
    }
}
