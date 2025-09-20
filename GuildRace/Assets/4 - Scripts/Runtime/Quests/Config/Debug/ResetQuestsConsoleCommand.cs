using AD.Services.Debug;
using System;
using VContainer;

namespace Game.Quests
{
    public class ResetQuestsConsoleCommand : ConsoleCommandHandler
    {
        private delegate void ResetQuests();

        private IQuestsService questsService;

        public override string Name => "reset_quests";
        public override string Desc => "Сброс и генерация новых квестов";
        public override Delegate Delegate => (ResetQuests)ResetQuestsCallback;

        [Inject]
        public void Inject(IQuestsService questsService)
        {
            this.questsService = questsService;
        }

        private void ResetQuestsCallback()
        {
            foreach (var module in questsService.Modules)
            {
                module.UpdateQuests();
            }
        }
    }
}