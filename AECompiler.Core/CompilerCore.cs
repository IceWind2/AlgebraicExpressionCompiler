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
        private IParser _parser;
        private IInterpreter _interpreter;

        public CompilerCore()
        {
            _parser = new Parser();
            _interpreter = new Interpreter(new BasicIdGenerator(), new RegisterDescriptor(),
                new LinuxAssemblyGenerator());
        }

        public void Compile(string expression)
        {
            ASTNode root = _parser.Parse(expression);

            _interpreter.Process(root);
        }
    }
}