using System;

using AECompiler.Core.Interpreters;
using AECompiler.Core.Lexers;
using AECompiler.Core.Parsers;

namespace AECompiler.ConsoleApp
{
    static class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                var g = "10 + 5*2-   2*2/(1+1)";

                var lex = new Lexer(g);

                var par = new Parser(lex);

                var itp = new Interpreter();

                Console.WriteLine(itp.Visit(par.Parse()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
