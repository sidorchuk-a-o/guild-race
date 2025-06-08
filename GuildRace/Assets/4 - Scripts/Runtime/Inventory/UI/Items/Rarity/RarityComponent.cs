using AD.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class RarityComponent : MonoBehaviour
    {
        [SerializeField] private Image colorImage;
        [SerializeField] private UIText nameText;

        public void Init(RarityDataVM rarityVM)
        {
            colorImage.color = rarityVM.Color;
            nameText.SetTextParams(rarityVM.NameKey);
        }
    }
}