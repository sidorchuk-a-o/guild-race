﻿using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.Instances
{
    public class SeasonVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public InstanceVM RaidVM { get; }
        public InstancesVM DungeonsVM { get; }

        public SeasonVM(SeasonInfo info)
        {
            Id = info.Id;

            NameKey = info.NameKey;
            DescKey = info.NameKey;

            RaidVM = new(info.Raid);
            DungeonsVM = new(info.Dungeons);
        }

        protected override void InitSubscribes()
        {
            RaidVM.AddTo(this);
            DungeonsVM.AddTo(this);
        }
    }
}