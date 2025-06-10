#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
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
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText guildRankText;
        [Space]
        [SerializeField] private UIText instanceStateText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            nicknameText.SetTextParams(ViewModel.Nickname);
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