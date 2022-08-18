using System;

using AECompiler.Core.AST.Nodes;
using AECompiler.Core.CodeGeneration.AssemblyGenerators;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.CodeGeneration.RegisterDescriptors;

namespace AECompiler.Core.Interpreters
{
    internal sealed class Interpreter : IInterpreter
    {
        private readonly IStoreIdGenerator _idGenerator;
        private readonly IRegisterDescriptor _registerDescriptor;
        private readonly IAssemblyGenerator _assemblyGenerator;

        public Interpreter
            (
                IStoreIdGenerator storeIdGenerator, 
                IRegisterDescriptor registerDescriptor, 
                IAssemblyGenerator assemblyGenerator
            )
        {
            _idGenerator = storeIdGenerator;
            _registerDescriptor = registerDescriptor;
            _assemblyGenerator = assemblyGenerator;
        }

        // AST processing start
        public void Process(ASTNode node)
        {
             Visit(node);
        }

        public StoreId Process(BinOpNode node)
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

        public StoreId Process(IntNode node)
        {
            var id = _idGenerator.GetNewId();
            
            var register = _registerDescriptor.StoreValue(id);
            
            Console.WriteLine($"Store {id}({node.GetValue()}) -> {register}");
            
            return id;
        }

        public StoreId Process(NoOpNode node)
        {
            return StoreId.Empty;
        }

        private StoreId Visit(ASTNode node)
        {
            return node.AcceptVisitor(this);
        }
    }
}
