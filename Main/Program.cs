using System;
using ExpressionAnalyzer;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var g = "10 + 5*2-   2*2/(1+1)";

                var Lex = new Lexer(g);

                var Par = new Parser(Lex);

                var Int = new Interpreter();

                Console.WriteLine(Int.Visit(Par.Parse()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
