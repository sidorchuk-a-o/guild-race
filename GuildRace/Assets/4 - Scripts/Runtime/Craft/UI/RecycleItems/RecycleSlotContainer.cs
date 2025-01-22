using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Craft
{
    public class RecycleSlotContainer : ItemSlotContainer
    {
        [Header("Reagent Preview")]
        [SerializeField] private Image reagentIconImage;
        [SerializeField] private UIText reagentNameText;
        [SerializeField] private UIText reagentCountText;

        private readonly CompositeDisp disp = new();

        private CraftVMFactory craftVMF;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        public override async void ShowPickupPreview(ItemVM itemVM, string state)
        {
            disp.Clear();
            disp.AddTo(this);

            var recyclingVM = craftVMF.GetRecyclingParams(itemVM.Id);

            if (recyclingVM != null)
            {
                recyclingVM.AddTo(disp);

                reagentIconImage.sprite = await recyclingVM.ReagentVM.LoadIcon();

                reagentNameText.SetTextParams(recyclingVM.ReagentVM.NameKey);
                reagentCountText.SetTextParams(recyclingVM.RecyclingResult);
            }

            base.ShowPickupPreview(itemVM, state);
        }

        public override void ResetPickupPreview()
        {
            base.ResetPickupPreview();

            disp.Clear();
        }
    }
}