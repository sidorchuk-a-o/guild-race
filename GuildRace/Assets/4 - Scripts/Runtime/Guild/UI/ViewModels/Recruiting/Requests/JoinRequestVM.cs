﻿using AD.Services.Router;
using Game.Inventory;

namespace Game.Guild
{
    public class JoinRequestVM : ViewModel
    {
        public string Id { get; }
        public bool IsDefault { get; }

        public CharacterVM CharacterVM { get; }

        public JoinRequestVM(
            JoinRequestInfo info,
            GuildVMFactory guildVMF,
            InventoryVMFactory inventoryVMF)
        {
            Id = info.Id;
            IsDefault = info.IsDefault;
            CharacterVM = new CharacterVM(info.Character, guildVMF, inventoryVMF, null);
        }

        protected override void InitSubscribes()
        {
            CharacterVM.AddTo(this);
        }
    }
}