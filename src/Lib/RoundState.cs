using Discord.WebSocket;
using System.Collections.Generic;

namespace Palermo.Lib
{
    public class RoundState
    {
        public int RoundCount { get; set; }
        public SocketUser[] Users { get; set; }
        public List<string> CustomRoles { get; set; }

        public RoundState()
        {
            RoundCount = 1;
        }
    }
}
