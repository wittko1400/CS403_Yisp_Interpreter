using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public class Environment
    {
        public readonly Environment Enclosing;
        private readonly Dictionary<string, object> Values = new();

        public Environment()
        {
            Enclosing = null;
        }

        public Environment(Environment enclosing)
        {
            Enclosing = enclosing;
        }

        public void Define(string name, object value)
        {
            Values[name] = value;
        }

        public object Get(Token name)
        {
            if (Values.ContainsKey(name.Lexeme))
            {
                return Values[name.Lexeme];
            }

            if (Enclosing != null)
            {
                return Enclosing.Get(name);
            }

            throw new RuntimeException($"Unknown symbol '{name.Lexeme}'.");
        }
    }
}
