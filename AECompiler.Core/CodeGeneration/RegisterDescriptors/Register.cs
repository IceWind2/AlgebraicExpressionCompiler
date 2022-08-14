namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal sealed class Register
    {
        public readonly RegisterName Name;
        public bool IsFree;
        
        public string StoredId { get; private set; }

        public Register(RegisterName name)
        {
            Name = name;
            IsFree = true;
            StoredId = null;
        }

        public void Store(string id)
        {
            IsFree = false;
            StoredId = id;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}