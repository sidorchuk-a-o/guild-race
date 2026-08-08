using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Quests
{
    public class QuestProgressContainer : MonoBehaviour
    {
        [SerializeField] private GameObject headerContainer;
        [Space]
        [SerializeField] private Slider slider;
        [SerializeField] private UIText counterText;

        public void Init(QuestVM questVM, CompositeDisp disp)
        {
            var isActive = questVM.RequiredProgress > 1;

            headerContainer.SetActive(isActive);
            gameObject.SetActive(isActive);

            if (!isActive)
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