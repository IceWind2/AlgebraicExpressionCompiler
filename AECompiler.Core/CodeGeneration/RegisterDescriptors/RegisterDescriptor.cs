using System;

using AECompiler.Core.CodeGeneration.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal sealed class RegisterDescriptor : IRegisterDescriptor
    {
        private readonly State _state;
        private int _counter;

        public RegisterDescriptor()
        {
            _counter = 0;
            _state = new State();
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

            return _state.GetRegister(_counter).Name;
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