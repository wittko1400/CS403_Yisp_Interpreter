using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _current = 0;


        public static readonly TokenType[] Operations =
        {
            TokenType.Plus, TokenType.Minus,
            TokenType.Star, TokenType.Slash,
            TokenType.Equal,
            TokenType.GreaterThan, TokenType.LessThan,
        };


        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public List<SExpr> Parse()
        {
            List<SExpr> expressions = new();

            while (!AtEnd())
            {
                try
                {
                    expressions.Add(SExpression());
                }
                catch (ParsingException)
                {
                    SynchronizeState();
                }
            }

            return expressions;
        }


        private SExpr SExpression()
        {
            if (MatchToken(TokenType.LeftParentheses))
            {
                return List();
            }
            else
            {
                return Atom();
            }
        }

        private SExpr Atom()
        {
            if (MatchToken(TokenType.Number, TokenType.String))
            {
                return new SExpr.Atom(PreviousToken().Literal);
            }
            else if (MatchToken(TokenType.Symbol) || MatchToken(Operations))
            {
                return new SExpr.Atom(PreviousToken());
            }
            else if (MatchToken(TokenType.SingleQuote))
            {
                List<SExpr> vals = new()
                {
                    new SExpr.Atom(new Token(TokenType.Symbol, "quote", "quote", PreviousToken().Line)),
                    SExpression(),
                };
                return new SExpr.List(vals);
            }

            throw Error(Peek(), "Expected atom.");
        }


        private SExpr List()
        {
            List<SExpr> values = new();
            while (!MatchToken(TokenType.RightParentheses))
            {
                values.Add(SExpression());
            }

            return new SExpr.List(values);
        }


        private bool MatchToken(params TokenType[] types)
        {
            foreach (TokenType type in types)
            {
                if (CheckToken(type))
                {
                    NextToken();
                    return true;
                }
            }

            return false;
        }


        private bool CheckToken(TokenType type)
        {
            if (AtEnd())
            {
                return false;
            }
            return Peek().Type == type;
        }

        private Token NextToken()
        {
            if (!AtEnd())
            {
                _current++;
            }
            return PreviousToken();
        }


        private bool AtEnd()
        {
            return Peek().Type == TokenType.Eof;
        }


        private Token Peek()
        {
            return _tokens[_current];
        }


        private Token PreviousToken()
        {
            return _tokens[_current - 1];
        }


        private ParsingException Error(Token token, string message)
        {
            Yisp.Error(token, message);
            return new ParsingException();
        }


        private void SynchronizeState()
        {
            NextToken();

            while (!AtEnd())
            {
                if (PreviousToken().Type == TokenType.RightParentheses)
                {
                    return;
                }

                switch (Peek().Type)
                {
                    case TokenType.LeftParentheses:
                        return;
                }

                NextToken();
            }
        }
    }
}
