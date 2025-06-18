using AD.UI;
using Game.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class AbilityItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltip;

        public async void Init(AbilityVM abilityVM, CancellationTokenSource ct)
        {
            var icon = await abilityVM.ThreatVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;

            if (nameText)
            {
                nameText.SetTextParams(abilityVM.NameKey);
            }

            tooltip.Init(abilityVM);
        }
    }
}