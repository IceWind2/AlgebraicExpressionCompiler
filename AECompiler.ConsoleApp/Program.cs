using System;

using AECompiler.Core.Interpreters;

namespace AECompiler.ConsoleApp
{
    static class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                var itp = new Interpreter();

                Console.WriteLine(itp.Compile("3 * 4 + 5 * 2   -   2*2/ (1 + 1)"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
