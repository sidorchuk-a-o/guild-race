using AD.Services.Router;

namespace Game.Guild
{
    public class GuildVMFactory : VMFactory
    {
        private readonly IGuildService guildService;

        public GuildVMFactory(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        public void CreateGuild(GuildEM guildEM)
        {
            guildService.CreateGuild(guildEM);
        }
    }
}