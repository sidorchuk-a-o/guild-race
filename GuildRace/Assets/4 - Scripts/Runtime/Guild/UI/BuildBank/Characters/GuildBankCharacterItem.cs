using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using System.Threading;
using UnityEngine;

namespace Game.Guild
{
    public class GuildBankCharacterItem : CharacterItem
    {
        [Header("Equip Slots")]
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(disp, ct);

            equipSlotsContainer.Init(ViewModel.EquiSlotsVM, disp);
        }
    }
}