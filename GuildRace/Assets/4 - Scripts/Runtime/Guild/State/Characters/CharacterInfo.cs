using Game.Items;
using UniRx;

namespace Game.Guild
{
    public class CharacterInfo
    {
        private readonly ReactiveProperty<GuildRankId> guildRankId = new();
        private readonly ReactiveProperty<SpecializationId> specId = new();

        private readonly ReactiveProperty<int> itemsLevel = new();
        private readonly IEquipSlotsCollection equipSlots;

        public string Id { get; }
        public string Nickname { get; }

        public ClassId ClassId { get; }
        public IReadOnlyReactiveProperty<SpecializationId> SpecId => specId;

        public IReadOnlyReactiveProperty<GuildRankId> GuildRankId => guildRankId;

        public IReadOnlyReactiveProperty<int> ItemsLevel => itemsLevel;
        public IEquipSlotsCollection EquipSlots => equipSlots;

        public CharacterInfo(string id, string nickname, ClassId classId, IEquipSlotsCollection equipSlots)
        {
            this.equipSlots = equipSlots;

            Id = id;
            Nickname = nickname;
            ClassId = classId;
        }

        public void SetSpecialization(SpecializationId value)
        {
            specId.Value = value;
        }

        public void SetGuildRank(GuildRankId value)
        {
            guildRankId.Value = value;
        }
    }
}