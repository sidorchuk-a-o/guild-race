using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.UI;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class KillCountContainer : MonoBehaviour
    {
        [SerializeField] private UIText countText;
        [SerializeField] private TooltipComponent tooltipComponent;

        public void Init(UnitVM unitVM, CompositeDisp disp)
        {
            unitVM.TotalCompletedCount
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            tooltipComponent.Init(unitVM);
        }

        private void CountChangedCallback(int count)
        {
            this.SetActive(count > 0);

            countText.SetTextParams(count);
        }
    }
}