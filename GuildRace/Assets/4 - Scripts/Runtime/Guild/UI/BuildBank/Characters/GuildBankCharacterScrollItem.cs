using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class GuildBankCharacterScrollItem : CharacterScrollItem
    {
        [SerializeField] private GameObject hasInstanceBlock;

        [Header("Equip Slots")]
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(disp, ct);

            equipSlotsContainer.Init(ViewModel.EquiSlotsVM, disp);

            ViewModel.InstanceVM
                .Subscribe(x => hasInstanceBlock.SetActive(ViewModel.HasInstance))
                .AddTo(disp);
        }
    }
}