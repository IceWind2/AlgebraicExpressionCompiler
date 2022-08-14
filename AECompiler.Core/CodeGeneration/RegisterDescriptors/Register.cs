using AECompiler.Core.Interpreters.IdGeneration;

namespace AECompiler.Core.CodeGeneration.RegisterDescriptors
{
    internal sealed class Register
    {
        public readonly RegisterName Name;
        public bool IsFree;
        
        public StoreId StoredId { get; private set; }

        public Register(RegisterName name)
        {
            Name = name;
            IsFree = true;
            StoredId = StoreId.Empty;
        }

        public void Store(StoreId id)
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