using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class TooltipItemStackComponent : MonoBehaviour
    {
        [SerializeField] private UIText sizeText;

        private ItemStackVM stackVM;
        private ItemCounterVM counterVM;
        private GuildVMFactory guildVMF;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;
        }

        public void Init(TooltipContext context, CompositeDisp disp)
        {
            if (context.ItemVM is IStackableItemVM stackableItemVM)
            {
                stackVM = stackableItemVM.StackVM;
                stackVM.ObserveChanged()
                    .Subscribe(UpdateSizeText)
                    .AddTo(disp);
            }

            counterVM = guildVMF.CreateItemCounter(context.DataVM.Id);
            counterVM.AddTo(disp);

            counterVM.Count
                .Subscribe(UpdateSizeText)
                .AddTo(disp);

            UpdateSizeText();
        }

        private void UpdateSizeText()
        {
            var sizeValue = stackVM != null
                ? $"{stackVM.Value}/{stackVM.Size} ({counterVM.Count})"
                : $"1 ({counterVM.Count})";

            sizeText.SetTextParams(sizeValue);

            this.SetActive(stackVM?.Value > 1 || counterVM.Count.Value > 1);
        }
    }
}