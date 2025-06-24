using AD.ToolsCollection;
using AD.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class EquipInGridComponent : ItemInGridComponent
    {
        [Header("Equip")]
        [SerializeField] private Image rarityImage;
        [SerializeField] private UIText levelText;
        [SerializeField] private UIText slotNameText;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var equipVM = itemVM as EquipItemVM;

            rarityImage.color = equipVM.DataVM.RarityVM.Color;

            levelText.SetTextParams(equipVM.DataVM.Level);
            slotNameText.SetTextParams(equipVM.DataVM.SlotVM.NameKey);
        }
    }
}