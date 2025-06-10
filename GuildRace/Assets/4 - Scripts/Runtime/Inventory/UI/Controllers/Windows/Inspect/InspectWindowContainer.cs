using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InspectWindowContainer : WindowContainer
    {
        [Header("Icon Preview")]
        [SerializeField] private Image iconImage;

        protected override async void InitWindow(CompositeDisp disp)
        {
            iconImage.sprite = await ItemVM.LoadIcon();
        }
    }
}