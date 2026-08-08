using System.Threading;
using AD.Game.UI;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Guild
{
    public class SpecItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltip;

        public async UniTask Init(SpecializationVM specVM, CancellationTokenSource ct)
        {
            var icon = await specVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(specVM.NameKey);

            tooltip.Init(specVM);
        }
    }
}