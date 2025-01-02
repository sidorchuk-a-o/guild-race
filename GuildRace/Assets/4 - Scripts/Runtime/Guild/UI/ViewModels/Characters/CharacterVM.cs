using AD.Services.Router;
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
        private readonly InstancesVMFactory instancesVMF;

        private readonly ReactiveProperty<SpecializationVM> specVM = new();
        private readonly ReactiveProperty<GuildRankVM> guildRankVM = new();
        private readonly ReactiveProperty<string> guildRankName = new();
        private readonly ReactiveProperty<ActiveInstanceVM> instanceVM = new();

        public string Id { get; }
        public string Nickname { get; }

        public ClassVM ClassVM { get; }

        public IReadOnlyReactiveProperty<SpecializationVM> SpecVM => specVM;

        public IReadOnlyReactiveProperty<string> GuildRankName => guildRankName;
        public IReadOnlyReactiveProperty<GuildRankVM> GuildRankVM => guildRankVM;

        public IReadOnlyReactiveProperty<int> ItemsLevel { get; }
        public ItemSlotsVM EquiSlotsVM { get; }

        public bool HasInstance => InstanceVM.Value != null;
        public IReadOnlyReactiveProperty<ActiveInstanceVM> InstanceVM => instanceVM;

        public CharacterVM(
            CharacterInfo info,
            GuildVMFactory guildVMF,
            InventoryVMFactory inventoryVMF,
            InstancesVMFactory instancesVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;
            this.instancesVMF = instancesVMF;

            Id = info.Id;
            Nickname = info.Nickname;
            ItemsLevel = info.ItemsLevel;

            ClassVM = guildVMF.GetClass(info.ClassId);
            EquiSlotsVM = inventoryVMF.CreateSlots(info.EquipSlots);
        }

        protected override void InitSubscribes()
        {
            ClassVM.AddTo(this);
            EquiSlotsVM.AddTo(this);

            info.SpecId
                .Subscribe(SpecializationChangedCallback)
                .AddTo(this);

            info.GuildRankId
                .Subscribe(GuildRankChangedCallback)
                .AddTo(this);

            info.InstanceId
                .Subscribe(InstanceChangedCallback)
                .AddTo(this);
        }

        private void SpecializationChangedCallback(SpecializationId specId)
        {
            specVM.Value?.ResetSubscribes();

            specVM.Value = guildVMF.GetSpecialization(specId);
            specVM.Value.AddTo(this);
        }

        private void GuildRankChangedCallback(GuildRankId guildRankId)
        {
            guildRankVM.Value?.ResetSubscribes();

            guildRankVM.Value = guildVMF.GetGuildRank(guildRankId);
            guildRankVM.Value.AddTo(this);

            guildRankVM.Value.Name
                .Subscribe(x => guildRankName.Value = x)
                .AddTo(guildRankVM.Value);
        }

        private void InstanceChangedCallback(string activeInstanceId)
        {
            instanceVM.Value = activeInstanceId.IsValid()
                ? instancesVMF.GetActiveInstance(activeInstanceId)
                : null;
        }
    }
}