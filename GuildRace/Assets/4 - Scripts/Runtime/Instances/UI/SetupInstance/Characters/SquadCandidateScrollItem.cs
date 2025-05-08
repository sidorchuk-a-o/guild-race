using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Instances
{
    public class SquadCandidateScrollItem : VMScrollItem<SquadCandidateVM>
    {
        [Header("Character")]
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private ThreatsContainer threatsContainer;

        [Header("Instance")]
        [SerializeField] private GameObject instanceContainer;
        [Space]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color otherInstanceColor;
        [SerializeField] private Color currentInstanceColor;

        private ActiveInstanceVM activeInstanceVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVM)
        {
            activeInstanceVM = instancesVM.GetSetupInstance();
        }

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            activeInstanceVM.AddTo(disp);

            // character 
            var characterVM = ViewModel.CharacterVM;

            nicknameText.SetTextParams(characterVM.Nickname);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);
            specNameText.SetTextParams(characterVM.SpecVM.NameKey);
            itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);

            // instance
            ViewModel.CharacterVM.InstanceVM
                .Subscribe(InstanceChangedCallback)
                .AddTo(disp);

            // threats
            await threatsContainer.Init(ViewModel.ThreatsVM, disp, ct);
        }

        private void InstanceChangedCallback(ActiveInstanceVM instanceVM)
        {
            var hasInstance = instanceVM != null;
            var sameInstance = hasInstance && instanceVM.Id == activeInstanceVM.Id;

            instanceContainer.SetActive(hasInstance);

            backgroundImage.color = sameInstance
                ? currentInstanceColor
                : otherInstanceColor;
        }
    }
}