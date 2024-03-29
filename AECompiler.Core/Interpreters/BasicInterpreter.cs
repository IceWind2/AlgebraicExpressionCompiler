﻿using System;

using AECompiler.Core.AST.Nodes;
using AECompiler.Core.AST.Tokens;
using AECompiler.Core.CodeGeneration.AssemblyGenerators;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.CodeGeneration.RegisterDescriptors;

namespace AECompiler.Core.Interpreters
{
    internal sealed class BasicInterpreter : IInterpreter
    {
        private readonly IAssemblyGenerator _assemblyGenerator;
        private readonly IStoreIdGenerator _idGenerator;
        private readonly IRegisterDescriptor _registerDescriptor;

        public BasicInterpreter
            (
                IAssemblyGenerator assemblyGenerator,
                IStoreIdGenerator storeIdGenerator,
                IRegisterDescriptor registerDescriptor
            )
        {
            _assemblyGenerator = assemblyGenerator;
            _idGenerator = storeIdGenerator;
            _registerDescriptor = registerDescriptor;
        }

        // AST processing start
        public void Process(ASTNode node)
        { 
            _assemblyGenerator.StartGeneration("output.S"); 
            
            var resultId = Visit(node);
            var resultRegister = _registerDescriptor.GetRegisterWithValue(resultId);
            if (resultRegister != RegisterName.AX)
            {
                _assemblyGenerator.WriteStore(RegisterName.AX.ToString(), resultRegister.ToString());
                _registerDescriptor.FreeRegister(resultRegister);
            }
            
            _assemblyGenerator.FinishGeneration();
        }

        public StoreId Process(BinOpNode node)
        {
            var child1 = node.GetChild(0);
            var child2 = node.GetChild(1);
            
            var id1 = Visit(child1);
            var id2 = Visit(child2);

            var regName1 = _registerDescriptor.GetRegisterWithValue(id1);
            var regName2 = _registerDescriptor.GetRegisterWithValue(id2);

            switch (node.GetToken().Type)
            {
                case TokenType.Plus:
                    _assemblyGenerator.WriteAdd(regName1.ToString(), regName2.ToString());
                    break;
                
                case TokenType.Minus:
                    _assemblyGenerator.WriteNeg(regName2.ToString());
                    _assemblyGenerator.WriteAdd(regName1.ToString(), regName2.ToString());
                    break;
                
                case TokenType.Mul:
                    if (regName1 == RegisterName.AX || regName2 == RegisterName.AX)
                    {
                        if (regName2 == RegisterName.AX)
                        {
                            regName2 = regName1;
                            id1 = id2;
                        }
                        
                        _assemblyGenerator.WriteMul(regName2.ToString());
                        break;
                    }

                    _registerDescriptor.StoreValue(id1, RegisterName.AX);
                    _assemblyGenerator.WriteStore(RegisterName.AX.ToString(), regName1.ToString());
                    _assemblyGenerator.WriteMul(regName2.ToString());
                    break;
                
                case TokenType.Div:
                    break;
               
                default:
                    throw new InvalidOperationException("Interpreter: wrong token type");
            }

            _registerDescriptor.FreeRegister(regName2);

            return id1;
        }

        public StoreId Process(IntNode node)
        {
            var id = _idGenerator.GetNewId();
            
            var register = _registerDescriptor.StoreValue(id);
            
            _assemblyGenerator.WriteStore(register.ToString(), node.GetValue().ToString());
            
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
