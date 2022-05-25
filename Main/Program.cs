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
                String g = "(-1 + (-1))* 2";

                var Lex = new Lexer(g);

                var Par = new Parser(Lex);

                Console.WriteLine(Par.Parse());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
