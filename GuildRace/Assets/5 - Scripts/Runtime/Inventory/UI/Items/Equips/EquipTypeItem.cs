using AD.UI;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class EquipTypeItem : MonoBehaviour
    {
        [SerializeField] private UIText nameText;

        public void Init(EquipTypeVM equipTypeVM)
        {
            nameText.SetTextParams(equipTypeVM.NameKey);
        }
    }
}