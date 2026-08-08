using AD.Router;
using AD.Store;
using VContainer;

namespace AD.Game.GuildLevels
{
    public class GuildLevelsVMFactory : VMFactory
    {
        private readonly IGuildLevelsService levelsService;
        private readonly IObjectResolver resolver;

        private StoreVMFactory storeVMF;

        public StoreVMFactory StoreVMF => storeVMF ??= resolver.Resolve<StoreVMFactory>();

        public GuildLevelsVMFactory(IGuildLevelsService levelsService, IObjectResolver resolver)
        {
            this.levelsService = levelsService;
            this.resolver = resolver;
        }

        public LevelsVM GetGuildLevels()
        {
            return new(levelsService.Levels, this);
        }
    }
}