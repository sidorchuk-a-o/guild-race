using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class ChanceConsumableMechanicContainer : ConsumableMechanicContainer
    {
        [Header("Chance Params")]
        [SerializeField] private UIText chanceText;

        private ChanceConsumableVM mechanicVM;

        public override async UniTask Init(ConsumablesItemVM consumableVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            mechanicVM = consumableVM.MechanicVM as ChanceConsumableVM;

            await base.Init(consumableVM, disp, ct);

            chanceText.SetTextParams(new(chanceText.LocalizeKey, mechanicVM.Chance));
        }
    }
}