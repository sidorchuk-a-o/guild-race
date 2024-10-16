﻿using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class CharacterVM : VMBase
    {
        private readonly CharacterInfo info;
        private readonly GuildVMFactory guildVMF;

        private readonly ReactiveProperty<SpecializationVM> specVM = new();
        private readonly ReactiveProperty<GuildRankVM> guildRankVM = new();
        private readonly ReactiveProperty<string> guildRankName = new();

        public string Id { get; }
        public string Nickname { get; }

        public ClassVM ClassVM { get; }
        public IReadOnlyReactiveProperty<SpecializationVM> SpecVM => specVM;

        public IReadOnlyReactiveProperty<string> GuildRankName => guildRankName;
        public IReadOnlyReactiveProperty<GuildRankVM> GuildRankVM => guildRankVM;

        public IReadOnlyReactiveProperty<int> ItemsLevel { get; }

        public CharacterVM(CharacterInfo info, GuildVMFactory guildVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;

            Id = info.Id;
            Nickname = info.Nickname;
            ItemsLevel = info.ItemsLevel;

            ClassVM = guildVMF.GetClass(info.ClassId);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            ClassVM.AddTo(disp);

            info.SpecId
                .Subscribe(SpecializationChangedCallback)
                .AddTo(disp);

            info.GuildRankId
                .Subscribe(GuildRankChangedCallback)
                .AddTo(disp);
        }

        private void SpecializationChangedCallback(SpecializationId specId)
        {
            specVM.Value?.ResetSubscribes();

            specVM.Value = guildVMF.GetSpecialization(specId);
            specVM.Value.AddTo(disp);
        }

        private void GuildRankChangedCallback(GuildRankId guildRankId)
        {
            guildRankVM.Value?.ResetSubscribes();

            guildRankVM.Value = guildVMF.GetGuildRank(guildRankId);
            guildRankVM.Value.AddTo(disp);

            guildRankVM.Value.Name
                .Subscribe(x => guildRankName.Value = x)
                .AddTo(guildRankVM.Value);
        }
    }
}