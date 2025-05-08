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
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private UIText nicknameText;
        [SerializeField] private ItemsGridContainer bagContainer;

        private readonly CompositeDisp unitDisp = new();

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

        public void SetSquadUnit(SquadUnitVM squadUnitVM, CompositeDisp disp)
        {
            unitDisp.Clear();
            unitDisp.AddTo(disp);

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