using AD.ToolsCollection;
using AD.UI;
using Game.Guild;
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

        private InstancesVMFactory instancesVMF;

        public CharacterVM CharacterVM { get; private set; }

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

        public void SetCharacter(CharacterVM characterVM)
        {
            CharacterVM = characterVM;

            var hasCharacter = characterVM != null;

            if (hasCharacter)
            {
                nicknameText.SetTextParams(characterVM.Nickname);
                classNameText.SetTextParams(characterVM.ClassVM.NameKey);
                itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
                specNameText.SetTextParams(characterVM.SpecVM.Value.NameKey);
            }

            characterItem.SetActive(hasCharacter);
        }

        private void SelectCallback()
        {
            if (CharacterVM != null)
            {
                instancesVMF.TryRemoveCharacterFromSquad(CharacterVM.Id);
            }
        }
    }
}