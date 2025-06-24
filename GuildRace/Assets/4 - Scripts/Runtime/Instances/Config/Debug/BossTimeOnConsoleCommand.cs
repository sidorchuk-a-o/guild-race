using AD.Services.Debug;
using AD.ToolsCollection;
using System;
using VContainer;

namespace Game.Instances
{
    public class BossTimeOnConsoleCommand : ConsoleCommandHandler
    {
        public delegate void SetBossTimeState();

        private IInstancesService instancesService;

        public override string Name => "boss_time_on";
        public override string Desc => "Включить время прохождения босса";
        public override Delegate Delegate => (SetBossTimeState)SetBossTimeStateCallback;

        [Inject]
        public void Inject(IInstancesService instancesService)
        {
            this.instancesService = instancesService;
        }

        private void SetBossTimeStateCallback()
        {
            instancesService.SetBossTimeState(true);

            this.LogMsg("Проверка времени для боссов: включено!");
        }
    }
}