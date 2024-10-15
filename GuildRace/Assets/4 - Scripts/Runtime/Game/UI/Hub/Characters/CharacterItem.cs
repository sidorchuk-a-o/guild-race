#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Game
{
    public class CharacterItem : VMScrollItem<CharacterVM>
    {
        [Header("Character")]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText guildRankText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            nicknameText.SetTextParams(ViewModel.Nickname);
            classNameText.SetTextParams(ViewModel.ClassVM.NameKey);

            ViewModel.ItemsLevel
                .Subscribe(x => itemsLevelText.SetTextParams(x))
                .AddTo(disp);

            ViewModel.GuildRankName
                .Subscribe(x => guildRankText.SetTextParams(x))
                .AddTo(disp);

            ViewModel.SpecVM
                .Subscribe(x => specNameText.SetTextParams(x.NameKey))
                .AddTo(disp);
        }
    }
}