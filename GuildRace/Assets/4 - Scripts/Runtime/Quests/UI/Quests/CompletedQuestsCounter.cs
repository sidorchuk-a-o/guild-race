using AD.ToolsCollection;
using Game.Components;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public class CompletedQuestsCounter : GameComponent<CompletedQuestsCounter>
    {
        [SerializeField] private CounterComponent counter;

        private CompletedQuestsVM completedQuestsVM;

        [Inject]
        public void Inject(QuestsVMFactory questsVMF)
        {
            completedQuestsVM = questsVMF.GetCompletedQuests();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            completedQuestsVM.AddTo(disp);

            counter.Init(completedQuestsVM.Count, disp);
        }
    }
}