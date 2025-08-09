using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Store;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
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
            imageContainer.SetActive(icon != null);

            itemText.SetTextParams(previewVM.TextData);
        }
    }
}