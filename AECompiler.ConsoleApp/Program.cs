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
             
                core.Compile("2 + (2*(3 + 7))");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
