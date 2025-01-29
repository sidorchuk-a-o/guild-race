using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Quests
{
    public class QuestProgressContainer : MonoBehaviour
    {
        [SerializeField] private UIText headerText;
        [Space]
        [SerializeField] private Slider slider;
        [SerializeField] private UIText counterText;

        public void Init(QuestVM questVM, CompositeDisp disp)
        {
            var isActive = questVM.RequiredProgress > 1;

            headerText.SetActive(isActive);
            gameObject.SetActive(isActive);

            if (isActive == false)
            {
                return;
            }

            slider.maxValue = questVM.RequiredProgress;

            questVM.ProgressStr
                .Subscribe(x => counterText.SetTextParams(x))
                .AddTo(disp);

            questVM.ProgressCounter
                .Subscribe(x => slider.value = x)
                .AddTo(disp);
        }
    }
}