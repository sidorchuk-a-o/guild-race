using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Instances
{
    public abstract class ConsumableMechanicContainer : MonoBehaviour
    {
        [SerializeField] private UIText descText;

        protected ConsumablesDataVM dataVM;

        public virtual UniTask Init(ConsumablesDataVM dataVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.dataVM = dataVM;

            descText.SetTextParams(GetDesc());

            return UniTask.CompletedTask;
        }

        protected virtual UITextData GetDesc()
        {
            return dataVM.DescKey;
        }
    }
}