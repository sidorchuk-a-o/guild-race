using AD.ToolsCollection;
using AD.UI;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Weekly
{
    public class WeeklyItem : MonoBehaviour
    {
        [SerializeField] private UIText valueText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltipComponent;

        private WeeklyConfig weeklyConfig;
        private WeeklyVM weeklyVM;

        [Inject]
        public void Inject(WeeklyConfig weeklyConfig, WeeklyVMFactory weeklyVMF)
        {
            this.weeklyConfig = weeklyConfig;

            weeklyVM = weeklyVMF.GetWeekly();
        }

        public void Init(CompositeDisp disp)
        {
            weeklyVM.AddTo(disp);

            valueText.SetTextParams($"{weeklyVM.CurrentDayOfWeek} / {weeklyConfig.DaysCount}");

            tooltipComponent.Init(weeklyVM);
        }
    }
}