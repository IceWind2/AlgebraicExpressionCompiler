namespace AECompiler.Core.CodeGeneration.AssemblyGenerators
{
    internal interface IAssemblyGenerator
    {
        public void StartGeneration(string filePath);

        public void FinishGeneration();

        public void WriteStore(string target, string source);

        public void WriteAdd(string target, string source);

        public void WriteNeg(string target);

        public void WriteMul(string operand);

        public void WriteDiv(string operand);

        public void WritePush(string source);
        
        public void WritePop(string target);

        public void WriteCall(string label);
    }
}