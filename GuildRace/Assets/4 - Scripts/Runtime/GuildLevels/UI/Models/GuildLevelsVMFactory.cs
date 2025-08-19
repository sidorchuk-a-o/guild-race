using AD.Services.Router;
using AD.Services.Store;
using VContainer;

namespace Game.GuildLevels
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
            return new LevelsVM(levelsService.Levels, this);
        }
    }
}