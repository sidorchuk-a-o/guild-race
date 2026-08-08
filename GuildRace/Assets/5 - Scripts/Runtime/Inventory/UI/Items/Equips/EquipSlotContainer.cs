using AD.ToolsCollection;
using AD.UI;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class EquipSlotContainer : ItemSlotContainer
    {
        [Header("Equip Diff")]
        [SerializeField] private UIText diffText;
        [SerializeField] private UIStates diffState;
        [SerializeField] private string positiveDiffKey = "default";
        [SerializeField] private string negativeDiffKey = "negative";

        protected override void Awake()
        {
            base.Awake();

            diffState.SetActive(false);
        }

        public override void ShowPickupPreview(ItemVM itemVM, PickupResult pickupResult)
        {
            base.ShowPickupPreview(itemVM, pickupResult);

            var checkPreview = pickupResult.Context.SelectedSlotVM == ViewModel;
            var checkPlacement = ViewModel.CheckPossibilityOfPlacement(itemVM);

            if (!checkPreview && checkPlacement)
            {
                var newEquip = (EquipItemVM)itemVM;
                var newLevel = newEquip.DataVM.Level;

                if (HasItem)
                {
                    var currEquip = (EquipItemVM)ViewModel.ItemVM.CurrentValue;
                    var currLevel = currEquip.DataVM.Level;

                    newLevel -= currLevel;
                }

                var hasDiff = newLevel != 0;
                var isPositive = newLevel > 0;
                var sign = isPositive ? "+" : string.Empty;

                diffText.SetTextParams($"{sign}{newLevel}");
                diffState.SetActive(hasDiff);

                if (hasDiff)
                {
                    var stateKey = isPositive
                        ? positiveDiffKey
                        : negativeDiffKey;

                    diffState.SetState(stateKey);
                }
            }
            else
            {
                diffText.SetTextParams(string.Empty);
                diffState.SetActive(false);
            }
        }

        public override void ResetPickupPreview()
        {
            diffState.SetActive(false);

            base.ResetPickupPreview();
        }
    }
}