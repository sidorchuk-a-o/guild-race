using AD.UI;
using Game.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class RoleItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltip;

        public async void Init(RoleVM subRoleVM, CancellationTokenSource ct)
        {
            var icon = await subRoleVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(subRoleVM.NameKey);

            if (tooltip)
            {
                tooltip.Init(subRoleVM);
            }
        }
    }
}