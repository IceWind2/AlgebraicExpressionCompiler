using AECompiler.Core.Interpreters.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal interface IRegisterDescriptor
    {
        public void StoreValue(StoreId id);

        public RegisterName GetRegisterWithValue(StoreId id);

        public void FreeRegister(RegisterName registerName);
    }
}