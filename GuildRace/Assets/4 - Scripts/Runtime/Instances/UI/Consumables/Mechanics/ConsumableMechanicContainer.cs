using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public abstract class ConsumableMechanicContainer : MonoBehaviour
    {
        [SerializeField] private UIText descText;

        protected ConsumablesItemVM consumableVM;
        private ConsumableMechanicVM mechanicVM;

        public virtual async UniTask Init(ConsumablesItemVM consumableVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.consumableVM = consumableVM;
            mechanicVM = consumableVM.MechanicVM;

            descText.SetTextParams(GetDesc());
        }

        protected virtual UITextData GetDesc()
        {
            return consumableVM.DescKey;
        }
    }
}