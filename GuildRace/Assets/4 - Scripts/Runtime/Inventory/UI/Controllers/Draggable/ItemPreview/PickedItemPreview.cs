using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class PickedItemPreview : MonoBehaviour
    {
        [SerializeField] private ItemIconImage iconImage;

        private readonly CompositeDisp disp = new();

        private void OnEnable()
        {
            disp.AddTo(this);
        }

        private void OnDisable()
        {
            disp.Clear();
        }

        public void ShowItem(ItemVM itemVM, bool iconStaticOn)
        {
            if (iconStaticOn)
            {
                transform.ApplyItemBounds(itemVM.BoundsVM.Value);
            }
            else
            {
                itemVM.BoundsVM.ObserveValue()
                    .Subscribe(x => transform.ApplyItemSize(x.size))
                    .AddTo(disp);
            }

            iconImage.UpdateIcon(itemVM, iconStaticOn, disp);

            this.SetActive(true);
        }
    }
}