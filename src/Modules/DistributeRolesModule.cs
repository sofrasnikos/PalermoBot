using Discord.Commands;
using Discord.WebSocket;
using Palermo.Services;
using System.Threading.Tasks;

namespace PalermoBot.Modules
{
    public class DistributeRolesModule : ModuleBase<SocketCommandContext>
    {
        private readonly DistributeRolesService _distributeRolesService;
        public DistributeRolesModule(DistributeRolesService distributeRolesService)
        {
            _distributeRolesService = distributeRolesService;
        }

        [Command("newround"), Alias("nr")]
        [Summary("Distributes Roles")]
        public async Task DistributeRoles(
        [Summary("The (optional) user to get info from")]
        params SocketUser[] users)
        {
            // todo if users = 0

            var roundCount = await _distributeRolesService.SendRolesAsync(users);

            await ReplyAsync($"[Round {roundCount}] Roles sent successfully to {users.Length} users");
        }
    }
}
