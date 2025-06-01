using AD.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class AbilityItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        public async void Init(AbilityVM abilityVM, CancellationTokenSource ct)
        {
            var icon = await abilityVM.ThreatVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(abilityVM.NameKey);
        }
    }
}