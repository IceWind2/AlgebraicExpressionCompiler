using System;
using System.Linq;
using System.Collections.Generic;
using AECompiler.Core.CodeGeneration.RegisterDescriptors;

namespace AECompiler.Core.CodeGeneration
{
    internal sealed class State
    {
        private readonly Register[] _registers;
        private readonly Stack<string> _stack;
        
        public State()
        {
            var registerNames = Enum.GetNames(typeof(RegisterName));
            _registers = new Register[registerNames.Length];
            
            for (var i = 0; i < registerNames.Length; ++i)
            {
                var regName = Enum.Parse<RegisterName>(registerNames[i]);
                _registers[i] = new Register(regName);
            }
            
            _stack = new Stack<string>();
        }

        public bool TryGetFreeRegister(out Register register)
        {
            register = _registers.FirstOrDefault(reg => reg.IsFree);

            return register is { };
        }

        public void ToStack(int idx)
        {
            _stack.Push(_registers[idx].StoredId);
        }

        public void UnloadStack()
        {
            for (var i = 0; _stack.Count > 0 && i < _registers.Length; ++i)
            {
                if (_registers[i].IsFree)
                {
                    _registers[i].Store(_stack.Pop());
                }
            }
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

        public int GetRegistersCount()
        {
            return _registers.Length;
        }
    }
}