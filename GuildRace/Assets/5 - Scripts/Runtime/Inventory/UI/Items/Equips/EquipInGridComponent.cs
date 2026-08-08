using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Inventory
{
    public class EquipInGridComponent : ItemInGridComponent
    {
        [Header("Equip")]
        [SerializeField] private Image rarityImage;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var equipVM = (EquipItemVM)itemVM;

            rarityImage.color = equipVM.DataVM.RarityVM.Color;
        }
    }
}