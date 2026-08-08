using System.Threading;
using AD.Store;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Store
{
    public class RewardPreviewItem : MonoBehaviour
    {
        [SerializeField] private GameObject imageContainer;
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText itemText;

        public async UniTask Init(RewardPreviewVM previewVM, CancellationTokenSource ct)
        {
            var icon = await previewVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            imageContainer.SetActive(icon is not null);

            itemText.SetTextParams(previewVM.TextData);
        }
    }
}