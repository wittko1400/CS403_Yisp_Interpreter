using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Envr = System.Environment;

namespace Yisp_Interpreter
{
    public static class Yisp
    {
        private static Interpreter interpreter = new();
        private static bool _hadError = false;
        private static bool _hadRuntimeError = false;

        public static void Main(string[] args)
        {

            if (args.Length > 1)
            {
                Console.WriteLine("Usage: Yisp_Interpreter [Yisp script]");
                Envr.Exit(64);
            }
            else if (args.Length == 1)
            {
                //RunFile(args[0]);
                RunTestRepo(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }
        public static void RunFile(string path)
        {
            string fileContents = string.Empty;
            try
            {
                fileContents = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: Could not access file '{path}', aborting. Reason: {e.Message}");
                Envr.Exit(65);
            }
            Run(fileContents);

            if (_hadError)
            {
                Envr.Exit(65);
            }
            if (_hadRuntimeError)
            {
                Envr.Exit(70);
            }
        }
        static void RunTestRepo(string repoPath)
        {
            string[] testFiles = Directory.GetFiles(repoPath, "*.txt", SearchOption.AllDirectories);
            int count = 0;
            int iterate = 0;
            foreach (var testFile in testFiles)
            {

                Console.WriteLine($"Running test: {testFile}");
                Console.WriteLine();
                var bytes = File.ReadAllBytes(testFile);
                Run(Encoding.UTF8.GetString(bytes));

                _hadError = false;
                _hadRuntimeError = false;

                iterate++;
                count = iterate;
                Console.WriteLine();
                Console.WriteLine($"Test {count}/22 Passed");
                Console.WriteLine();

            }

            if (_hadError) System.Environment.Exit(65);
            if (_hadRuntimeError) System.Environment.Exit(70);
        }
        public static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (line == null)
                {
                    break;
                }
                Run(line);
                _hadError = false;
            }
        }

        public static void Run(string source)
        {
            Scanner scanner = new(source);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new(tokens);
            List<SExpr> expressions = parser.Parse();

            if (_hadError)
            {
                return;
            }

            interpreter.Interpret(expressions);
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        public static void Error(Token token, string message)
        {
            if (token.Type == TokenType.Eof)
            {
                Report(token.Line, " at end", message);
            }
            else
            {
                Report(token.Line, $" at '{token.Lexeme}'", message);
            }
        }

        public static void RuntimeError(RuntimeException error)
        {
            Console.WriteLine($"RuntimeException: {error.Message}");
            _hadRuntimeError = true;
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
            _hadError = true;
        }

        public static void DebugReset()
        {
            interpreter = new();
            _hadError = false;
            _hadRuntimeError = false;
        }
    }
}