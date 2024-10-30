using AD.Services.Router;
using AD.ToolsCollection;
using Game.Inventory;
using UniRx;

namespace Game.Guild
{
    public class CharacterVM : ViewModel
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
        public ItemSlotsVM EquiSlotsVM { get; }

        public CharacterVM(CharacterInfo info, GuildVMFactory guildVMF, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;

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
    }
}