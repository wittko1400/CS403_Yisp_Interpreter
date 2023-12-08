using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Token
    {
        public readonly TokenType Type;
        public readonly string Lexeme;
        public readonly object Literal;
        public readonly int Line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }

        public override string ToString()
        {
            return $"{Type} {Lexeme ?? "null"} {Literal ?? "null"}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Token t)
            {
                return (Type == t.Type) && Lexeme.Equals(t.Lexeme) && Literal.Equals(t.Literal) && (Line == t.Line);
            }
            else
            {
                return false;
            }
        }
    }
}
