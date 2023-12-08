using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Scanner
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new();
        private int _startIndex = 0;
        private int _currentIndex = 0;
        private int _line = 1;

        public Scanner(string source)
        {
            _source = source;
        }


        public List<Token> ScanTokens()
        {
            while (!AtEnd())
            {
                _startIndex = _currentIndex;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.Eof, "", null, _line));
            return _tokens;
        }

        private void ScanToken()
        {
            char c = NextCharacter();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LeftParentheses);
                    break;
                case ')':
                    AddToken(TokenType.RightParentheses);
                    break;
                case '+':
                    AddToken(TokenType.Plus);
                    break;
                case '-':
                    AddToken(TokenType.Minus);
                    break;
                case '*':
                    AddToken(TokenType.Star);
                    break;
                case '/':
                    AddToken(TokenType.Slash);
                    break;
                case '=':
                    AddToken(TokenType.Equal);
                    break;
                case '<':
                    AddToken(TokenType.LessThan);
                    break;
                case '>':
                    AddToken(TokenType.GreaterThan);
                    break;
                case '\'':
                    AddToken(TokenType.SingleQuote);
                    break;
                case ';':
                    while (Peek() != '\n' && !AtEnd())
                    {
                        NextCharacter();
                    }
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    _line++;
                    break;

                case '"':
                    ReadString();
                    break;

                default:
                    if (IsDigit(c))
                    {
                        ReadNumber();
                    }
                    else if (IsAlpha(c))
                    {
                        ReadSymbol();
                    }
                    else
                    {
                        Yisp.Error(_line, "Unexpected character.");
                    }
                    break;
            }
        }

        private bool AtEnd()
        {
            return _currentIndex >= _source.Length;
        }

        private char NextCharacter()
        {
            return _source[_currentIndex++];
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }


        private void AddToken(TokenType type, object literal)
        {
            string lexeme = _source[_startIndex.._currentIndex];
            _tokens.Add(new Token(type, lexeme, literal, _line));
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsAlpha(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '-' || c == '?';
        }

        private static bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        private char Peek()
        {
            if (AtEnd())
            {
                return '\0';
            }
            return _source[_currentIndex];
        }

        private char PeekNext()
        {
            if (_currentIndex + 1 >= _source.Length)
            {
                return '\0';
            }
            return _source[_currentIndex + 1];
        }

        private void ReadNumber()
        {
            while (IsDigit(Peek()))
            {
                NextCharacter();
            }

            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                NextCharacter();
                while (IsDigit(Peek()))
                {
                    NextCharacter();
                }
            }

            double value = double.Parse(_source[_startIndex.._currentIndex]);
            AddToken(TokenType.Number, value);
        }


        private void ReadString()
        {
            while (Peek() != '"' && !AtEnd())
            {
                if (Peek() == '\n')
                {
                    _line++;
                }
                NextCharacter();
            }

            if (AtEnd())
            {
                Yisp.Error(_line, "Unterminated string.");
                return;
            }

            NextCharacter();

            string value = _source[(_startIndex + 1)..(_currentIndex - 1)];
            AddToken(TokenType.String, value);
        }

        private void ReadSymbol()
        {
            while (IsAlphaNumeric(Peek()))
            {
                NextCharacter();
            }

            string text = _source[_startIndex.._currentIndex];
            AddToken(TokenType.Symbol, text);
        }
    }
}
