using System.Threading;
using AD.Game.UI;
using AD.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Guild
{
    public class ClassItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltip;

        public async void Init(ClassVM classVM, CancellationTokenSource ct)
        {
            var icon = await classVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(classVM.NameKey);

            tooltip.Init(classVM);
        }
    }
}