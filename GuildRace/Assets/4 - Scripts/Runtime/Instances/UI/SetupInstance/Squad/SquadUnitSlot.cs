using AD.UI;
using AD.ToolsCollection;
using Game.Guild;
using Game.Inventory;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SquadUnitSlot : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private GameObject characterItem;
        [SerializeField] private UIButton selectButton;
        [Space]
        [SerializeField] private NicknameComponent nicknameComponent;
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private ThreatsContainer threatsContainer;
        [SerializeField] private ChanceDiffContainer chanceDiffContainer;
        [SerializeField] private ItemsGridContainer bagContainer;

        private readonly CompositeDisp unitDisp = new();
        private CancellationTokenSource ct;

        private InstancesVMFactory instancesVMF;

        public bool HasUnit => SquadUnitVM != null;
        public SquadUnitVM SquadUnitVM { get; private set; }

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            characterItem.SetActive(false);

            selectButton.OnClick
                .Subscribe(SelectCallback)
                .AddTo(this);
        }

        public async void SetSquadUnit(SquadUnitVM squadUnitVM, CompositeDisp disp)
        {
            unitDisp.Clear();
            unitDisp.AddTo(disp);

            var token = new CancellationTokenSource();

            ct?.Cancel();
            ct = token;

            SquadUnitVM = squadUnitVM;

            var hasUnit = squadUnitVM != null;

            if (hasUnit)
            {
                var characterVM = squadUnitVM.CharactedVM;

                nicknameComponent.Init(characterVM);
                classNameText.SetTextParams(characterVM.ClassVM.NameKey);
                itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
                specNameText.SetTextParams(characterVM.SpecVM.NameKey);

                bagContainer.Init(squadUnitVM.BagVM, unitDisp);
                chanceDiffContainer.Init(squadUnitVM, unitDisp);

                await threatsContainer.Init(squadUnitVM.ResolvedThreatsVM, unitDisp, token);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            characterItem.SetActive(hasUnit);

            SubscribeToDraggableController(unitDisp);
        }

        private void SelectCallback()
        {
            if (SquadUnitVM != null)
            {
                instancesVMF.TryRemoveCharacterFromSquad(SquadUnitVM.CharactedVM.Id);
            }
        }

        private void SubscribeToDraggableController(CompositeDisp disp)
        {
            var draggableController = InventoryDraggableController.GetComponent();

            draggableController.OnPickupItem
                .Subscribe(OnPickupItemCallback)
                .AddTo(disp);

            draggableController.OnReleaseItem
                .Subscribe(OnReleaseItemCallback)
                .AddTo(disp);
        }

        private void OnPickupItemCallback()
        {
            threatsContainer.SetActive(false);
        }

        private void OnReleaseItemCallback()
        {
            threatsContainer.SetActive(true);
        }
    }
}