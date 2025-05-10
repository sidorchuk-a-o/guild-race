using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
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
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private ThreatsContainer threatsContainer;
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

                nicknameText.SetTextParams(characterVM.Nickname);
                classNameText.SetTextParams(characterVM.ClassVM.NameKey);
                itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
                specNameText.SetTextParams(characterVM.SpecVM.NameKey);

                bagContainer.Init(squadUnitVM.BagVM, unitDisp);

                await threatsContainer.Init(squadUnitVM.ResolvedThreatsVM, unitDisp, token);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            characterItem.SetActive(hasUnit);
        }

        private void SelectCallback()
        {
            if (SquadUnitVM != null)
            {
                instancesVMF.TryRemoveCharacterFromSquad(SquadUnitVM.CharactedVM.Id);
            }
        }
    }
}