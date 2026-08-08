using AD.Localization;
using AD.Router;
using R3;

namespace AD.Game.Guild
{
    public class GuildVM : ViewModel
    {
        private readonly IGuildService guildService;
        private readonly ReactiveProperty<bool> rosterIsFull = new();

        public ReadOnlyReactiveProperty<string> GuildName { get; }
        public EmblemVM EmblemVM { get; }

        public ReadOnlyReactiveProperty<string> PlayerName { get; }
        public LocalizeKey PlayerRank { get; }

        public ReadOnlyReactiveProperty<bool> RosterIsFull => rosterIsFull;

        public GuildVM(IGuildService guildService, GuildVMFactory guildVMF)
        {
            this.guildService = guildService;

            GuildName = guildService.GuildName;
            EmblemVM = guildVMF.GetEmblem(guildService.Emblem);

            PlayerName = guildService.PlayerName;
            PlayerRank = guildService.GuildRanks.GuildMaster.NameKey;
        }

        protected override void InitSubscribes()
        {
            EmblemVM.AddTo(this);

            guildService.Characters
                .ObserveCountChanged()
                .Subscribe(CharactersCountChanged)
                .AddTo(this);

            CharactersCountChanged(guildService.Characters.Count);
        }

        private void CharactersCountChanged(int count)
        {
            rosterIsFull.Value = count >= guildService.MaxCharactersCount.CurrentValue;
        }
    }
}