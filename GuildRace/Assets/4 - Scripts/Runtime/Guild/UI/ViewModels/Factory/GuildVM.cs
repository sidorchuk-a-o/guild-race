﻿using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class GuildVM : ViewModel
    {
        private readonly GuildConfig guildConfig;
        private readonly IGuildService guildService;

        private readonly ReactiveProperty<bool> rosterIsFull = new();

        public IReadOnlyReactiveProperty<string> GuildName { get; }
        public EmblemVM EmblemVM { get; }

        public IReadOnlyReactiveProperty<string> PlayerName { get; }
        public IReadOnlyReactiveProperty<string> PlayerRank { get; }

        public IReadOnlyReactiveProperty<bool> RosterIsFull => rosterIsFull;

        public GuildVM(GuildConfig guildConfig, IGuildService guildService)
        {
            this.guildConfig = guildConfig;
            this.guildService = guildService;

            GuildName = guildService.GuildName;
            EmblemVM = new(guildService.Emblem, guildConfig.EmblemParams);

            PlayerName = guildService.PlayerName;
            PlayerRank = guildService.GuildRanks.GuildMaster.Name;
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
            rosterIsFull.Value = count >= guildConfig.MaxCharactersCount;
        }
    }
}