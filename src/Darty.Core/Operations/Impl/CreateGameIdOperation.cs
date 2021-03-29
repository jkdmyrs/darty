namespace Darty.Core.Operations.Impl
{
    using Darty.Core.Operations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CreateGameIdOperation : ICreateGameIdOperation
    {
        public async Task<string> Execute()
        {
            char[] chars = "ABCDEFGHJKMNPQRSTUVWXYZ123456789".ToCharArray();
            var rng = new Random();
            char GetRandomChar() => chars[rng.Next(chars.Length)];

            List<char> gameId = new List<char>();
            for (int i = 0; i < 6; i++)
            {
                gameId.Add(GetRandomChar());
            }

            return new string(gameId.ToArray());
        }
    }
}
