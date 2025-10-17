using AD.Services.Router;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Instances;
using Game.Inventory;
using UniRx;

namespace Game.Guild
{
    public class CharacterVM : ViewModel
    {
        private readonly CharacterInfo info;
        private readonly GuildVMFactory guildVMF;

        private readonly ReactiveProperty<GuildRankVM> guildRankVM = new();
        private readonly ReactiveProperty<LocalizeKey> guildRankName = new();
        private readonly ReactiveProperty<ActiveInstanceVM> instanceVM = new();

        public string Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey SurnameKey { get; }
        public GenderType Gender { get; }

        public ClassVM ClassVM { get; }
        public SpecializationVM SpecVM { get; }

        public IReadOnlyReactiveProperty<LocalizeKey> GuildRankName => guildRankName;
        public IReadOnlyReactiveProperty<GuildRankVM> GuildRankVM => guildRankVM;

        public IReadOnlyReactiveProperty<int> ItemsLevel { get; }
        public ItemSlotsVM EquiSlotsVM { get; }

        public bool HasInstance => InstanceVM.Value != null;
        public IReadOnlyReactiveProperty<ActiveInstanceVM> InstanceVM => instanceVM;

        public CharacterVM(CharacterInfo info, GuildVMFactory guildVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;

            Id = info.Id;
            NameKey = info.NameKey;
            SurnameKey = info.SurnameKey;
            Gender = info.Gender;
            ItemsLevel = info.ItemsLevel;

            ClassVM = guildVMF.GetClass(info.ClassId);
            SpecVM = guildVMF.GetSpecialization(info.SpecId);
            EquiSlotsVM = guildVMF.InventoryVMF.CreateSlots(info.EquipSlots);
        }

        protected override void InitSubscribes()
        {
            ClassVM.AddTo(this);
            SpecVM.AddTo(this);
            EquiSlotsVM.AddTo(this);

            info.GuildRankId
                .Subscribe(GuildRankChangedCallback)
                .AddTo(this);

            info.InstanceId
                .Subscribe(InstanceChangedCallback)
                .AddTo(this);
        }

        private void GuildRankChangedCallback(GuildRankId guildRankId)
        {
            guildRankVM.Value?.ResetSubscribes();

            guildRankVM.Value = guildVMF.GetGuildRank(guildRankId);
            guildRankVM.Value.AddTo(this);

            guildRankName.Value = guildRankVM.Value.NameKey;
        }

        private void InstanceChangedCallback(string activeInstanceId)
        {
            instanceVM.Value = activeInstanceId.IsValid()
                ? guildVMF.InstancesVMF.GetActiveInstance(activeInstanceId)
                : null;
        }
    }
}