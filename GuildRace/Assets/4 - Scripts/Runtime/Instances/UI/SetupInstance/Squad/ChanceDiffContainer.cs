using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class ChanceDiffContainer : MonoBehaviour
    {
        [SerializeField] private UIText diffText;
        [SerializeField] private UIStates diffState;
        [SerializeField] private string positiveDiffKey = "positive";
        [SerializeField] private string negativeDiffKey = "negative";

        private InstancesVMFactory instancesVMF;
        private SquadUnitVM squadUnitVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Init(SquadUnitVM squadUnitVM, CompositeDisp disp)
        {
            this.squadUnitVM = squadUnitVM;

            var draggableController = InventoryDraggableController.GetComponent();

            draggableController.OnPickupItem
                .Subscribe(OnPickupItemCallback)
                .AddTo(disp);

            draggableController.OnReleaseItem
                .Subscribe(OnReleaseItemCallback)
                .AddTo(disp);
        }

        private void OnPickupItemCallback(PickupResult result)
        {
            if (result.SelectedItemVM is not ConsumablesItemVM)
            {
                return;
            }

            var addItemArgs = new AddItemArgs
            {
                CharacterId = squadUnitVM.CharactedId,
                ConsumablesId = result.SelectedItemVM.Id
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
                : UISelectionStateComponent.defaultStateKey;

            diffText.SetTextParams(diffStr);
            diffState.SetState(stateKey);

            gameObject.SetActive(true);
        }

        private void OnReleaseItemCallback()
        {
            gameObject.SetActive(false);
        }
    }
}