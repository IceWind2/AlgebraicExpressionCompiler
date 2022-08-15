using System;
using System.IO;
using System.Text;
using AECompiler.Core.CodeGeneration.AssemblyGenerators.Constants;

namespace AECompiler.Core.CodeGeneration.AssemblyGenerators
{
    internal sealed class LinuxAssemblyGenerator : IAssemblyGenerator
    {
        private FileStream _output;  //TODO: handle dispose

        public void StartGeneration(string filePath)
        {
            _output = File.Open(filePath, FileMode.Create);
        }
        
        public void FinishGeneration()
        {
            _output.Close();
        }

        public void WriteHeader()
        {
            WriteToFile(LinuxConstants.Header);
        }

        public void WriteData()
        {
            WriteToFile(LinuxConstants.Data);
        }

        public void WriteOutputFunction(string label)
        {
            WriteToFile(string.Format(LinuxConstants.Output, label));
        }

        public void WriteStart()
        {
            WriteToFile(LinuxConstants.Start);
        }

        public void WriteStore(string target, string source)
        {
            WriteToFile(string.Format(LinuxConstants.Store, target, source));
        }

        public void WriteAdd(string target, string source)
        {
            WriteToFile(string.Format(LinuxConstants.Add, target, source));
        }

        public void WriteMul(string operand)
        {
            WriteToFile(string.Format(LinuxConstants.Mul, operand));
        }

        public void WriteDiv(string operand)
        {
            WriteToFile(string.Format(LinuxConstants.Div, operand));

        }

        public void WritePush(string source)
        {
            WriteToFile(string.Format(source));
        }

        public void WritePop(string target)
        {
            WriteToFile(string.Format(target));
        }

        public void WriteCall(string label)
        {
            WriteToFile(string.Format(LinuxConstants.Call, label));
        }

        public void WriteExit()
        {
            WriteToFile(String.Format(LinuxConstants.Exit));
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