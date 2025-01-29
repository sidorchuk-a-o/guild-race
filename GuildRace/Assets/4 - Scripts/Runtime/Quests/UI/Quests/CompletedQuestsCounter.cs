using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Components;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public class CompletedQuestsCounter : GameComponent<CompletedQuestsCounter>
    {
        [SerializeField] private GameObject viewRoot;
        [SerializeField] private UIText counterText;

        private CompletedQuestsVM completedQuestsVM;

        [Inject]
        public void Inject(QuestsVMFactory questsVMF)
        {
            completedQuestsVM = questsVMF.GetCompletedQuests();
        }

        protected override void Awake()
        {
            base.Awake();

            viewRoot.SetActive(false);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            completedQuestsVM.AddTo(disp);

            completedQuestsVM.Count
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(completedQuestsVM.Count.Value);
        }

        private void CountChangedCallback(int count)
        {
            viewRoot.SetActive(count > 0);

            counterText.SetTextParams(count);
        }
    }
}