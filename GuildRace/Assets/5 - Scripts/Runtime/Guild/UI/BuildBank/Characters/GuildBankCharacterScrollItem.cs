using System.Threading;
using AD.Game.Inventory;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace AD.Game.Guild
{
    public class GuildBankCharacterScrollItem : CharacterScrollItem
    {
        [SerializeField] private GameObject hasInstanceBlock;

        [Header("Equip Slots")]
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(disp, ct);

            equipSlotsContainer.Init(ViewModel.EquipSlotsVM, disp);

            ViewModel.InstanceVM
                .Subscribe(x => hasInstanceBlock.SetActive(x != null))
                .AddTo(disp);
        }
    }
}