using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class CharacterInfo
    {
        private readonly ReactiveProperty<GuildRankId> guildRankId = new();
        private readonly ReactiveProperty<SpecializationId> specId = new();
        private readonly ReactiveProperty<int> itemsLevel = new();
        private readonly ReactiveProperty<string> instanceId = new();

        private readonly ItemSlotsCollection equipSlots;

        public string Id { get; }
        public string Nickname { get; }

        public ClassId ClassId { get; }
        public IReadOnlyReactiveProperty<SpecializationId> SpecId => specId;

        public IReadOnlyReactiveProperty<GuildRankId> GuildRankId => guildRankId;

        public IReadOnlyReactiveProperty<int> ItemsLevel => itemsLevel;
        public IItemSlotsCollection EquipSlots => equipSlots;

        public bool HasInstance => instanceId.Value.IsValid();
        public IReadOnlyReactiveProperty<string> InstanceId => instanceId;

        public CharacterInfo(string id, string nickname, ClassId classId, IEnumerable<EquipSlotInfo> equipSlots)
        {
            this.equipSlots = new(equipSlots);

            Id = id;
            Nickname = nickname;
            ClassId = classId;
        }

        public void Init()
        {
            foreach (var slot in equipSlots)
            {
                slot.Item.SilentSubscribe(UpdateItemsLevel);
            }

            UpdateItemsLevel();
        }

        public void SetSpecialization(SpecializationId value)
        {
            specId.Value = value;
        }

        public void SetGuildRank(GuildRankId value)
        {
            guildRankId.Value = value;
        }

        public void SetInstanceId(string value)
        {
            instanceId.Value = value;
        }

        private void UpdateItemsLevel()
        {
            var count = 0;
            var level = 0;

            foreach (var slot in equipSlots)
            {
                if (slot.Item.Value is EquipItemInfo equip)
                {
                    count++;
                    level += equip.Level;
                }
            }

            itemsLevel.Value = level / Mathf.Max(1, count);
        }
    }
}