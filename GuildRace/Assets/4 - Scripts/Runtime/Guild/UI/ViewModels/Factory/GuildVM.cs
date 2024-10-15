﻿using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class GuildVM : VMBase
    {
        private readonly GuildConfig guildConfig;
        private readonly IGuildService guildService;

        private readonly ReactiveProperty<bool> rosterIsFull = new();

        public IReadOnlyReactiveProperty<string> GuildName { get; }

        public IReadOnlyReactiveProperty<string> PlayerNickname { get; }
        public IReadOnlyReactiveProperty<string> PlayerRank { get; }

        public IReadOnlyReactiveProperty<bool> RosterIsFull => rosterIsFull;

        public GuildVM(GuildConfig guildConfig, IGuildService guildService)
        {
            this.guildConfig = guildConfig;
            this.guildService = guildService;

            GuildName = guildService.Name;

            PlayerNickname = new ReactiveProperty<string>("< NICKNAME >");
            PlayerRank = guildService.GuildRanks.GuildMaster.Name;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            guildService.Characters
                .ObserveCountChanged()
                .Subscribe(CharactersCountChanged)
                .AddTo(disp);

            CharactersCountChanged(guildService.Characters.Count);
        }

        private void CharactersCountChanged(int count)
        {
            rosterIsFull.Value = count >= guildConfig.MaxCharactersCount;
        }
    }
}