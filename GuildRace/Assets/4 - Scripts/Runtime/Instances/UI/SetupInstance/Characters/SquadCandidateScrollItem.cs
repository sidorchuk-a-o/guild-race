using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SquadCandidateScrollItem : VMScrollItem<SquadCandidateVM>
    {
        [Header("Character")]
        [SerializeField] private NicknameComponent nicknameComponent;
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private UIText guildRankText;
        [Space]
        [SerializeField] private ThreatsContainer threatsContainer;

        [Header("Instance")]
        [SerializeField] private GameObject instanceContainer;
        [Space]
        [SerializeField] private UIStates blockState;
        [SerializeField] private string hasInstanceKey = "hasInstance";
        [SerializeField] private string hasGroupKey = "hasGroup";

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

            nicknameComponent.Init(characterVM);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);
            specNameText.SetTextParams(characterVM.SpecVM.NameKey);
            itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
            guildRankText.SetTextParams(characterVM.GuildRankName.Value);

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
            var stateName = hasInstance ? sameInstance ? hasGroupKey : hasInstanceKey : "default";

            blockState.SetState(stateName);
        }
    }
}