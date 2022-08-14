namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal interface IRegisterDescriptor
    {
        public void StoreValue(string id);

        public RegisterName GetRegisterWithValue(string id);

        public void FreeRegister(RegisterName registerName);
    }
}