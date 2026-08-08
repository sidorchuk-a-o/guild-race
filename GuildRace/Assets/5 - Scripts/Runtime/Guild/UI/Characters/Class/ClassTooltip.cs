using System.Threading;
using AD.Game.Inventory;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Guild
{
    public class ClassTooltip : TooltipContainer
    {
        [Header("Class Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;
        [SerializeField] private SpecItem[] specItems;
        [SerializeField] private EquipTypeItem armorTypeItem;
        [SerializeField] private EquipTypeItem weaponTypeItem;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var classVM = (ClassVM)viewModel;
            var icon = await classVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(classVM.NameKey);
            descText.SetTextParams(classVM.DescKey);

            armorTypeItem.Init(classVM.ArmorTypeVM);
            weaponTypeItem.Init(classVM.WeaponTypeVM);

            for (var i = 0; i < specItems.Length; i++)
            {
                var specItem = specItems[i];
                var specVM = classVM.SpecializationsVM[i];

                await specItem.Init(specVM, ct);
            }
        }
    }
}