using System.Linq;
using AD.ToolsCollection;
using VContainer;

namespace Game.Instances
{
    public class ThreatConsumableHandler : ConsumableMechanicHandler
    {
        private InstancesConfig instancesConfig;

        [Inject]
        public void Inject(InstancesConfig instancesConfig)
        {
            this.instancesConfig = instancesConfig;
        }

        public ThreatId GetThreat(ConsumablesItemInfo item)
        {
            return (ThreatId)item.MechanicParams[0].IntParse();
        }

        public override void Invoke(ConsumablesItemInfo item, SquadChanceParams squadParams)
        {
            var setupInstance = squadParams.SetupInstance;

            var resolvedThreadId = GetThreat(item);
            var checkThreat = setupInstance.BossUnit.Threats.Contains(resolvedThreadId);

            if (checkThreat)
            {
                var instanceType = setupInstance.Instance.Type;
                var chanceParams = instancesConfig.CompleteChanceParams.GetParams(instanceType);

                var consumableChance = chanceParams.GetConsumableChance(item.Rarity);

                squadParams.ConsumableChance += consumableChance.ChanceMod * item.Stack.Value / item.Stack.Size;
            }
        }
    }
}