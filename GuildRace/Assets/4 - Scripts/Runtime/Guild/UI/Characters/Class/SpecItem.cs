using AD.UI;
using Cysharp.Threading.Tasks;
using Game.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
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