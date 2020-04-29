using Discord;
using Discord.WebSocket;
using Palermo.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palermo.Services
{
    public class DistributeRolesService
    {
        private int roundCount = 1;
        public async Task<int> SendRolesAsync(params SocketUser[] users)
        {
            var roundLabel = "Round "; // todo move to config
            var roles = BuildRolesList(users.Length);
            var tasks = new List<Task>();
            int i = 0;

            foreach (var user in users)
            {
                var message = $"[{roundLabel} {roundCount}]  **{roles[i++]}**";
                tasks.Add(Task.Run(() => user.SendMessageAsync(message)));
            }

            await Task.WhenAll(tasks);
            return roundCount++;
        }

        private List<string> BuildRolesList(int n)
        {
            var hiddenKillerString = "Κρυφός δολοφόνος"; // todo move to config
            var knownKillerString = "Φανερός δολοφόνος";
            var civilianString = "Πολίτης";
            var snitchString = "Αστυνόμος";
            var returnValue = new List<string>();
            var killers = n / 3;

            returnValue.Add(hiddenKillerString);
            returnValue.Add(snitchString);

            for (int i = 2; i < killers; i++)
            {
                returnValue.Add(knownKillerString);
            }

            for (int i = killers + 2; i < n; i++)
            {
                returnValue.Add(civilianString);
            }

            returnValue.Shuffle();
            
            return returnValue;
        }
    }
}
