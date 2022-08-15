using System;

using AECompiler.Core.Interpreters;

namespace AECompiler.ConsoleApp
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                var itp = new Interpreter();

                itp.Compile("3 * 4 + 5 * 2   -   2*2/ (1 + 1)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
