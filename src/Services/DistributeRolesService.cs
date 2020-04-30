using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Palermo.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palermo.Services
{
    public class DistributeRolesService
    {
        private int roundCount = 1;

        private readonly IConfigurationRoot _config;

        public DistributeRolesService(IConfigurationRoot config)
        {
            _config = config;
        }

        public async Task<int> SendRolesAsync(params SocketUser[] users)
        {
            var roles = BuildRolesList(users.Length);
            var tasks = new List<Task>();
            int i = 0;

            foreach (var user in users)
            {
                var message = $"[Round {roundCount}]  **{roles[i++]}**";
                tasks.Add(Task.Run(() => user.SendMessageAsync(message)));
            }

            await Task.WhenAll(tasks);
            return roundCount++;
        }

        private List<string> BuildRolesList(int n)
        {
            var hiddenKiller = _config["labels:hiddenKiller"];
            var killer = _config["labels:killer"];
            var civilian = _config["labels:civilian"];
            var policeman = _config["labels:policeman"];
            var returnValue = new List<string>();
            var killers = n / 3;

            returnValue.Add(hiddenKiller);
            returnValue.Add(policeman);

            for (int i = 2; i < killers; i++)
            {
                returnValue.Add(killer);
            }

            for (int i = killers + 2; i < n; i++)
            {
                returnValue.Add(civilian);
            }

            returnValue.Shuffle();
            
            return returnValue;
        }
    }
}
