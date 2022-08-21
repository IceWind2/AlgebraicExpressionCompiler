using System.IO;
using System.Text;

using AECompiler.Core.CodeGeneration.AssemblyGenerators.Constants;

namespace AECompiler.Core.CodeGeneration.AssemblyGenerators
{
    internal sealed class LinuxAssemblyGenerator : IAssemblyGenerator
    {
        private FileStream _output;  //TODO: handle dispose
        private const string Indent = "    ";
        

        public void StartGeneration(string filePath)
        {
            _output = File.Open(filePath, FileMode.Create);
            
            WriteToFile(LinuxConstants.Header);
            WriteToFile(string.Format(LinuxConstants.OutputFunc, LinuxConstants.OutputLabel));
            
            WriteToFile(LinuxConstants.Start);
        }
        
        public void FinishGeneration()
        {
            WriteCall(LinuxConstants.OutputLabel);
            
            WriteToFile(LinuxConstants.Exit);
            
            WriteToFile(LinuxConstants.Data);
            
            _output.Close();
        }

        public void WriteStart()
        {
            WriteToFile(LinuxConstants.Start);
        }

        public void WriteStore(string target, string source)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Store, target, source));
        }

        public void WriteAdd(string target, string source)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Add, target, source));
        }

        public void WriteNeg(string target)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Neg, target));
        }

        public void WriteMul(string operand)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Mul, operand));
        }

        public void WriteDiv(string operand)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Div, operand));
        }

        public void WritePush(string source)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Push, source));
        }

        public void WritePop(string target)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Pop, target));
        }

        public void WriteCall(string label)
        {
            WriteToFile(Indent + string.Format(LinuxConstants.Call, label));
        }

        public void WriteExit()
        {
            WriteToFile(LinuxConstants.Exit);
        }

        private void WriteToFile (string text)
        {
            var data = new UTF8Encoding(true).GetBytes(text);
            
            _output.Write(data, 0, data.Length);
        }
        
        ~LinuxAssemblyGenerator()
        {
            _output.Dispose();
        }
    }
}