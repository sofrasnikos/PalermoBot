using Discord.Commands;
using Discord.WebSocket;
using Palermo.Lib;
using Palermo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalermoBot.Modules
{
    public class DistributeRolesModule : ModuleBase<SocketCommandContext>
    {
        private static readonly RoundState roundState = new RoundState();
        private readonly DistributeRolesService _distributeRolesService;
        public DistributeRolesModule(DistributeRolesService distributeRolesService)
        {
            _distributeRolesService = distributeRolesService;
        }

        [Command("newround"), Alias("nr")]
        [Summary("Distributes Roles")]
        public async Task DistributeRoles(
        [Summary("The users who will play this round")]
        params SocketUser[] users)
        {
            if (users.Length == 0)
            {
                await ReplyAsync("Please specify users");
                return;
            }
            roundState.Users = users;
            var roundCount = await _distributeRolesService.SendRolesAsync(roundState);

            await ReplyAsync($"[Round {roundCount}] Roles sent successfully to {users.Length} users");
        }

        [Command("setroles"), Alias("sr")]
        [Summary("Sets comma-separated custom roles")]
        public async Task SetCustomRoles([Remainder] [Summary("The comma-separated custom roles")] string remainder)
        {
            string[] roles = remainder.Split(",");

            roundState.CustomRoles = new List<string>(roles);

            await ReplyAsync($"Set {roles.Length} custom roles");
        }
    }
}
