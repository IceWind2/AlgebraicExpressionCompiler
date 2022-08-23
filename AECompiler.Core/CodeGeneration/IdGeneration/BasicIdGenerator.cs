using System;
using System.Linq;

namespace AECompiler.Core.CodeGeneration.IdGeneration
{
    internal sealed class BasicIdGenerator : IStoreIdGenerator
    {
        private uint _counter;
        private readonly Random _random;
        private const int IdLength = 6;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public BasicIdGenerator()
        {
            _counter = 0;
            _random = new Random();
        }

        public StoreId GetNewId()
        {
            _counter++;
            
            var randomStr = new string(Enumerable.Repeat(Chars, IdLength)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray()
            );

            var id = $"id#{randomStr}";

            return new StoreId(id, _counter);
        }
    }
}