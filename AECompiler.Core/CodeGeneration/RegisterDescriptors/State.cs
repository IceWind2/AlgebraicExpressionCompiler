using System;
using System.Collections.Generic;
using System.Linq;

using AECompiler.Core.CodeGeneration.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal sealed class State
    {
        private readonly Register[] _registers;
        private readonly Stack<StoreId> _stack;
        
        public State()
        {
            var registerNames = Enum.GetNames(typeof(RegisterName));
            _registers = new Register[registerNames.Length];
            
            for (var i = 0; i < registerNames.Length; ++i)
            {
                var regName = Enum.Parse<RegisterName>(registerNames[i]);
                _registers[i] = new Register(regName);
            }
            
            _stack = new Stack<StoreId>();
        }

        public bool TryGetFreeRegister(out Register register)
        {
            register = _registers.FirstOrDefault(reg => reg.IsFree);

            return register is { };
        }

        public void ToStack(int idx)
        {
            _stack.Push(_registers[idx].StoredId);
            _registers[idx].Clear();
        }

        public bool TryGetFromStack(out Register register)
        {
            var idx = Array.FindIndex(_registers, register => register.IsFree);
            
            if (_stack.Count == 0 || idx == -1)
            {
                register = null;
                return false;
            }

            _registers[idx].Store(_stack.Pop());
            register = _registers[idx];
            return true;
        }
        
        public Register GetRegister(int idx)
        {
            if (idx < 0 || idx > _registers.Length)
            {
                throw new InvalidOperationException("GetRegister: Register doesnt exist");
            }
            
            return _registers[idx];
        }

        public Register GetRegister(RegisterName registerName)
        {
            return _registers.First(reg => reg.Name == registerName);
        }

        public int GetRegisterIdx(RegisterName registerName)
        {
            var idx = Array.FindIndex(_registers, register => register.Name == registerName);

            if (idx == -1)
            {
                throw new InvalidOperationException("GetRegister: Register doesnt exist");
            }

            return idx;
        }
        
        public int GetRegistersCount()
        {
            return _registers.Length;
        }
    }
}