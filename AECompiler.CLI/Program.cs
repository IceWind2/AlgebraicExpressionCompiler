using System;
using AECompiler.Core;

namespace AECompiler.CLI
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            try
            {
                var core = new CompilerCore();
             
                core.Compile(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
