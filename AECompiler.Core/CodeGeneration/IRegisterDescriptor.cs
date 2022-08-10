namespace AECompiler.Core.CodeGeneration
{
    interface IRegisterDescriptor
    {
        public void StoreValue(string id);

        public RegisterName GetRegisterWithValue(string id);

        public void FreeRegister(RegisterName registerName);
    }
}