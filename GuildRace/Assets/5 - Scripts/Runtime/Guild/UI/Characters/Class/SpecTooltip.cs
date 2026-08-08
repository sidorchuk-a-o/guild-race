using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Guild
{
    public class SpecTooltip : TooltipContainer
    {
        [Header("Spec Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;
        [SerializeField] private RoleItem roleItem;
        [SerializeField] private SubRoleItem subRoleItem;
        [SerializeField] private AbilitiesContainer abilitiesContainer;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var specVM = (SpecializationVM)viewModel;
            var icon = await specVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(specVM.NameKey);
            descText.SetTextParams(specVM.DescKey);

            roleItem.Init(specVM.RoleVM, ct);
            subRoleItem.Init(specVM.SubRoleVM, ct);

            abilitiesContainer.Init(specVM.AbilitiesVM, ct);
        }
    }
}