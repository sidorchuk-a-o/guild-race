using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class ItemIconImage : MonoBehaviour
    {
        [Header("Icon")]
        [SerializeField] private Image iconImage;
        [SerializeField] private RectOffset iconPaddings;

        [Header("Loading")]
        [SerializeField] private GameObject loadingIndicator;

        private ItemVM itemVM;

        private void OnEnable()
        {
            iconImage.SetActive(itemVM != null);
        }

        private void OnDisable()
        {
            iconImage.SetActive(false);
        }

        public async void UpdateIcon(ItemVM itemVM, bool iconStaticOn, CompositeDisp disp)
        {
            this.itemVM = itemVM;

            iconImage.SetActive(false);

            if (itemVM == null)
            {
                return;
            }

            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(true);
            }

            // upd rect
            if (iconStaticOn)
            {
                SizeValueChanged();
            }
            else
            {
                itemVM.BoundsVM.ObserveValue()
                    .Subscribe(SizeValueChanged)
                    .AddTo(disp);
            }

            // upd sprite
            iconImage.sprite = await itemVM.LoadIcon();

            iconImage.SetActive(true);

            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }

        private void SizeValueChanged()
        {
            var iconRect = iconImage.transform as RectTransform;
            var parentRect = iconRect.parent as RectTransform;

            var isRotated = itemVM.BoundsVM.IsRotated;
            var parentSize = parentRect.sizeDelta;

            iconRect.sizeDelta = new Vector2
            {
                x = (isRotated ? parentSize.y : parentSize.x) - iconPaddings.left - iconPaddings.right,
                y = (isRotated ? parentSize.x : parentSize.y) - iconPaddings.top - iconPaddings.bottom
            };

            iconRect.localRotation = isRotated
                ? Quaternion.Euler(0, 0, -90)
                : Quaternion.identity;
        }
    }
}