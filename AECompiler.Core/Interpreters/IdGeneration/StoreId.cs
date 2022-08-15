namespace AECompiler.Core.Interpreters.IdGeneration
{
    public readonly struct StoreId
    {
        public static readonly StoreId Empty = new StoreId("", 0);
        
        public readonly string Id;
        public readonly uint SerialNumber;

        public StoreId(string id, uint serialNumber)
        {
            Id = id;
            SerialNumber = serialNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is StoreId other && SerialNumber == other.SerialNumber && Id.Equals(other.Id);
        }

        public static bool operator ==(StoreId first, StoreId second) => first.SerialNumber == second.SerialNumber &&
                                                                         first.Id.Equals(second.Id);

        public static bool operator !=(StoreId first, StoreId second) => !(first == second);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SerialNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Id;
        }
    }
}