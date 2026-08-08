using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class ItemPreviewContainer : MonoBehaviour
    {
        [SerializeField] private RectTransform emptyContainer;
        [SerializeField] private RectTransform previewContainer;

        private void Awake()
        {
            SetActive(false);
        }

        public void PlaceItem(ItemInSlotComponent item)
        {
            var itemTransform = (RectTransform)item.transform;

            itemTransform.SetParent(previewContainer);
            itemTransform.anchoredPosition = Vector2.zero;
            itemTransform.localScale = Vector2.one;

            SetActive(true);
        }

        public void RemoveItem()
        {
            SetActive(false);
        }

        private void SetActive(bool hasItem)
        {
            emptyContainer.SetActive(!hasItem);
            previewContainer.SetActive(hasItem);
        }
    }
}