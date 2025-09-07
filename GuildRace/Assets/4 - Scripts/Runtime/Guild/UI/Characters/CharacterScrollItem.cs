using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class CharacterScrollItem : VMScrollItem<CharacterVM>
    {
        [Header("Character")]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private ClassPreviewComponent classPreviewComponent;
        [SerializeField] private NicknameComponent nicknameComponent;
        [SerializeField] private UIText guildRankText;
        [Space]
        [SerializeField] private UIText instanceStateText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            await classPreviewComponent.Init(ViewModel, ct);

            if (ct.IsCancellationRequested) return;

            nicknameComponent.Init(ViewModel);

            classNameText.SetTextParams(ViewModel.ClassVM.NameKey);
            specNameText.SetTextParams(ViewModel.SpecVM.NameKey);

            ViewModel.ItemsLevel
                .Subscribe(x => itemsLevelText.SetTextParams(x))
                .AddTo(disp);

            ViewModel.InstanceVM
                .Subscribe(x => instanceStateText.SetActive(ViewModel.HasInstance))
                .AddTo(disp);

            if (guildRankText)
            {
                ViewModel.GuildRankName
                    .Subscribe(x => guildRankText.SetTextParams(x))
                    .AddTo(disp);
            }
        }
    }
}