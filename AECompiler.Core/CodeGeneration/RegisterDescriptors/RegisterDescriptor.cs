using System;
using AECompiler.Core.CodeGeneration.AssemblyGenerators;
using AECompiler.Core.CodeGeneration.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal sealed class RegisterDescriptor : IRegisterDescriptor
    {
        private readonly State _state;
        private readonly IAssemblyGenerator _assemblyGenerator;
        private int _counter;

        public RegisterDescriptor(IAssemblyGenerator assemblyGenerator)
        {
            _counter = 0;
            _state = new State();
            _assemblyGenerator = assemblyGenerator;
        }

        public RegisterName StoreValue(StoreId id)
        {
            if (_state.TryGetFreeRegister(out var register))
            {
                register.Store(id);
                _counter = _state.GetRegisterIdx(register.Name);
                return register.Name;
            }

            _counter = ++_counter % _state.GetRegistersCount();
            _state.ToStack(_counter);
            _state.GetRegister(_counter).Store(id);
            _assemblyGenerator.WritePush(_state.GetRegister(_counter).ToString());
            
            return _state.GetRegister(_counter).Name;
        }
        
        public void StoreValue(StoreId id, RegisterName registerName)
        {
            var register = _state.GetRegister(registerName);

            if (!register.IsFree)
            {
                _state.ToStack(_state.GetRegisterIdx(registerName));
            }
            
            register.Store(id);
        }

        public RegisterName GetRegisterWithValue(StoreId id)
        {
            for (var i = 0; i < _state.GetRegistersCount(); ++i)
            {
                if (_state.GetRegister(i).StoredId.Equals(id))
                {
                    return _state.GetRegister(i).Name;
                }
            }

            while (_state.TryGetFromStack(out var register))
            {
                _assemblyGenerator.WritePop(register.ToString());
                
                if (register.StoredId == id)
                {
                    return register.Name;
                }
            }
            
            throw new InvalidOperationException("GetRegisterWithValue: Value not found");
        }

        public void FreeRegister(RegisterName registerName)
        {
            var register = _state.GetRegister(registerName);
            register.Clear();
        }
    }
}