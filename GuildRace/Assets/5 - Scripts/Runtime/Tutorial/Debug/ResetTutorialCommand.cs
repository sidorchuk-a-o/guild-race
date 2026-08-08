using System;
using AD.AppDebug;
using VContainer;

namespace AD.Game.Tutorial
{
    public class ResetTutorialCommand : ConsoleCommandHandler
    {
        public delegate void ResetTutorial();

        private ITutorialService tutorialService;

        public override string Name => "reset_tutorial";
        public override string Desc => "Сброс прогресса туториала";
        public override Delegate Delegate => (ResetTutorial)ResetTutorialCallback;

        [Inject]
        public void Inject(ITutorialService tutorialService)
        {
            this.tutorialService = tutorialService;
        }

        private void ResetTutorialCallback()
        {
            tutorialService.ResetProgress();
        }
    }
}