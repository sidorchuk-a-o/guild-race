using AD.Game.UI;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Instances
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