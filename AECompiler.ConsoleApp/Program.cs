using System;
using AECompiler.Core;

namespace AECompiler.ConsoleApp
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                var core = new CompilerCore();
             
                core.Compile("1+(2*(3+(4+(5+(6+(7+8+9-10))))))");
                // core.Compile("3 * 4 + 5 * 2   -   2*2/ (1 + 1)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
