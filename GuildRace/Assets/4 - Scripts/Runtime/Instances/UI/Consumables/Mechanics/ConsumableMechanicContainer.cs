#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

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

        protected ConsumablesDataVM dataVM;

        public virtual async UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.dataVM = dataVM;

            descText.SetTextParams(GetDesc());
        }

        protected virtual UITextData GetDesc()
        {
            return dataVM.DescKey;
        }
    }
}