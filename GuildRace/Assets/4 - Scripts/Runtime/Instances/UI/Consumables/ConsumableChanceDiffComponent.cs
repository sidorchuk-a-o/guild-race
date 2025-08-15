using AD.UI;
using Game.Inventory;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class ConsumableChanceDiffComponent : MonoBehaviour
    {
        [SerializeField] private UIText diffText;
        [SerializeField] private UIStates diffState;
        [SerializeField] private string positiveDiffKey = "positive";
        [SerializeField] private string negativeDiffKey = "negative";

        private InstancesVMFactory instancesVMF;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public void Init(TooltipContext context)
        {
            var addItemArgs = new AddItemArgs
            {
                ConsumablesId = context.ItemVM.Id
            };

            var diff = instancesVMF.CalcChanceDiff(addItemArgs);

            var hasChance = diff != 0;
            var isPositive = diff > 0;
            var sign = isPositive ? "+" : string.Empty;

            var diffStr = hasChance
                ? $"{sign}{diff}%"
                : "-";

            var stateKey = hasChance
                ? isPositive ? positiveDiffKey : negativeDiffKey
                : UISelectable.defaultStateKey;

            diffText.SetTextParams(diffStr);
            diffState.SetState(stateKey);
        }
    }
}