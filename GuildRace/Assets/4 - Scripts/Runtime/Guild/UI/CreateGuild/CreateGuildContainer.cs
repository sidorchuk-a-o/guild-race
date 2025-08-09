using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class CreateGuildContainer : UIContainer
    {
        [Header("Guild")]
        [SerializeField] private UIInputField playerNameField;
        [SerializeField] private UIInputField guildNameField;
        [SerializeField] private UIButton createButton;

        [Header("Emblem")]
        [SerializeField] private EmblemParamsContainer emblemParamsContainer;

        private GuildVMFactory guildVMF;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;
        }

        private void Awake()
        {
            guildNameField.Value
                .Subscribe(UpdateCreateButtonState)
                .AddTo(this);

            playerNameField.Value
                .Subscribe(UpdateCreateButtonState)
                .AddTo(this);

            createButton.OnClick
                .Subscribe(CreateCallback)
                .AddTo(this);
        }

        private void UpdateCreateButtonState()
        {
            var hasGuildName = guildNameField.Value.Value.IsValid();
            var hasPlayerName = playerNameField.Value.Value.IsValid();

            createButton.SetInteractableState(hasPlayerName && hasGuildName);
        }

        private void CreateCallback()
        {
            var guildEM = new GuildEM
            {
                GuildName = guildNameField.Value.Value,
                PlayerName = playerNameField.Value.Value,
                Emblem = emblemParamsContainer.EmblemEM
            };

            guildVMF.CreateOrUpdateGuild(guildEM);

            Router.Push(
                pathKey: RouteKeys.Hub.Recruiting,
                loadingKey: LoadingScreenKeys.Loading,
                parameters: RouteParams.FirstRoute);
        }
    }
}