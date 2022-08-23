using AECompiler.Core.CodeGeneration.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal interface IRegisterDescriptor
    {
        public RegisterName StoreValue(StoreId id);

        public void StoreValue(StoreId id, RegisterName registerName);

        public RegisterName GetRegisterWithValue(StoreId id);

        public void FreeRegister(RegisterName registerName);
    }
}