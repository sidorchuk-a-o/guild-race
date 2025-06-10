using AD.ToolsCollection;
using AD.UI;
using UnityEngine;

namespace Game.Inventory
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
                var newEquip = itemVM as EquipItemVM;
                var newLevel = newEquip.DataVM.Level;

                if (HasItem)
                {
                    var currEquip = ViewModel.ItemVM.Value as EquipItemVM;
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
        }

        public override void ResetPickupPreview()
        {
            diffState.SetActive(false);

            base.ResetPickupPreview();
        }
    }
}