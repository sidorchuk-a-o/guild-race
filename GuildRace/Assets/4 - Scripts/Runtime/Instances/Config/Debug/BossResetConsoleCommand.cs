using AD.Services.Debug;
using System;
using System.Linq;
using VContainer;

namespace Game.Instances
{
    public class BossResetConsoleCommand : ConsoleCommandHandler
    {
        public delegate void ResetBoss();

        private IInstancesService instancesService;

        public override string Name => "reset_boss";
        public override string Desc => "Сбросить счетчик прохождения всех боссов";
        public override Delegate Delegate => (ResetBoss)ResetBossCallback;

        [Inject]
        public void Inject(IInstancesService instancesService)
        {
            this.instancesService = instancesService;
        }

        private void ResetBossCallback()
        {
            var bossUnits = instancesService.Seasons
                .SelectMany(x => x.GetInstances())
                .SelectMany(x => x.BossUnits);

            foreach (var bossUnit in bossUnits)
            {
                bossUnit.ResetCompletedState();
            }
        }
    }
}