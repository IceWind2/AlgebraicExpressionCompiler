using System;

namespace AECompiler.Core.CodeGeneration
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

        public void StoreValue(string id)
        {
            if (_state.TryGetFreeRegister(out var register))
            {
                register.Store(id);
            }
            else
            {
                _counter = ++_counter % _state.GetRegistersCount();
                _state.ToStack(_counter);
                _state.GetRegister(_counter).Store(id);
            }
        }

        public RegisterName GetRegisterWithValue(string id)
        {
            _state.UnloadStack();
            
            for (var i = 0; i < _state.GetRegistersCount(); ++i)
            {
                if (_state.GetRegister(i).StoredId.Equals(id))
                {
                    return _state.GetRegister(i).Name;
                }
            }

            throw new InvalidOperationException("GetRegisterWithValue: Value not found");
        }

        public void FreeRegister(RegisterName registerName)
        {
            var register = _state.GetRegister(registerName);
            register.IsFree = true;
        }
    }
}