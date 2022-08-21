using AECompiler.Core.AST.Nodes;
using AECompiler.Core.CodeGeneration.AssemblyGenerators;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.CodeGeneration.RegisterDescriptors;
using AECompiler.Core.Interpreters;
using AECompiler.Core.Parsers;

namespace AECompiler.Core
{
    public class CompilerCore
    {
        private readonly IParser _parser;
        private readonly IInterpreter _interpreter;

        public CompilerCore()
        {
            _parser = new RecursiveParser();

            var assemblyGenerator = new LinuxAssemblyGenerator();
            var basicIdGenerator = new BasicIdGenerator();
            var registerDescriptor= new RegisterDescriptor(assemblyGenerator);

            _interpreter = new BasicInterpreter(assemblyGenerator, basicIdGenerator, registerDescriptor);
        }

        public void Compile(string expression)
        {
            ASTNode root = _parser.Parse(expression);

            _interpreter.Process(root);
        }
    }
}