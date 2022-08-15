using System;
using AECompiler.Core.AST.Nodes;
using AECompiler.Core.CodeGeneration.RegisterDescriptors;
using AECompiler.Core.Interpreters.IdGeneration;
using AECompiler.Core.Lexers;
using AECompiler.Core.Parsers;

namespace AECompiler.Core.Interpreters
{
    public class Interpreter
    {
        private readonly IParser _parser;
        private readonly IStoreIdGenerator _idGenerator;
        private readonly IRegisterDescriptor _registerDescriptor;

        public Interpreter()
        {
            ILexer lexer = new Lexer();
            _parser = new Parser(lexer);
            _idGenerator = new BasicIdGenerator();
            _registerDescriptor = new RegisterDescriptor();
        }

        public void Compile(string expression)
        {
            Visit(_parser.Parse(expression));
        }

        internal StoreId Process(BinOpNode node)
        {
            node.TryGetChild(0, out var child1);
            node.TryGetChild(1, out var child2);

            var id1 = Visit(child1);
            var id2 = Visit(child2);

            var regName1 = _registerDescriptor.GetRegisterWithValue(id1);
            var regName2 = _registerDescriptor.GetRegisterWithValue(id2);
            
            Console.WriteLine($"{node.GetToken().Type} {id1} {id2} -> {regName1} ({id1})");
            
            _registerDescriptor.FreeRegister(regName2);

            return id1;
        }

        internal StoreId Process(IntNode node)
        {
            var id = _idGenerator.GetNewId();
            
            var register = _registerDescriptor.StoreValue(id);
            
            Console.WriteLine($"Store {id}({node.GetValue()}) -> {register}");
            
            return id;
        }

        internal StoreId Process(NoOpNode node)
        {
            return StoreId.Empty;
        }
        
        private StoreId Visit(ASTNode node)
        {
            return node.AcceptVisitor(this);
        }
    }
}
