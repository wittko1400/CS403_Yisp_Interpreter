using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public enum TokenType
    {

        LeftParentheses,
        RightParentheses,
        SingleQuote,
        Plus,
        Minus,
        Star,
        Slash,
        Equal,
        LessThan,
        GreaterThan,
        Symbol,
        String,
        Number,
        Eof,
    }
}
