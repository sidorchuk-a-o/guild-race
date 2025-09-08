using AD.UI;
using AD.ToolsCollection;
using Game.UI;
using UnityEngine;
using VContainer;
using UniRx;

namespace Game.Weekly
{
    public class WeeklyItem : MonoBehaviour
    {
        [SerializeField] private UIText valueText;

        [Header("Tooltip")]
        [SerializeField] private TooltipComponent tooltipComponent;

        private WeeklyVM weeklyVM;

        [Inject]
        public void Inject(WeeklyVMFactory weeklyVMF)
        {
            weeklyVM = weeklyVMF.GetWeekly();
        }

        public void Init(CompositeDisp disp)
        {
            weeklyVM.AddTo(disp);

            weeklyVM.CurrentDayOfWeek
                .Subscribe(x => valueText.SetTextParams($"{x} / {weeklyVM.DaysCount}"))
                .AddTo(disp);

            tooltipComponent.Init(weeklyVM);
        }
    }
}