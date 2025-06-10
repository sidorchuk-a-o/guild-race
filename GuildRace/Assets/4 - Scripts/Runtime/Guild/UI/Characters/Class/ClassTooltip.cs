using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
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
            var classVM = viewModel as ClassVM;
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