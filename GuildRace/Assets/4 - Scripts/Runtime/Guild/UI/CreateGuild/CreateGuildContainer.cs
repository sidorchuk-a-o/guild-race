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
        [SerializeField] private UIInputField nameField;
        [SerializeField] private UIButton createButton;

        private GuildVMFactory guildVMF;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;
        }

        private void Awake()
        {
            nameField.Value
                .Subscribe(NameValueChanged)
                .AddTo(this);

            createButton.OnClick
                .Subscribe(CreateCallback)
                .AddTo(this);
        }

        private void NameValueChanged(string value)
        {
            createButton.SetInteractableState(value.IsValid());
        }

        private void CreateCallback()
        {
            var guildEM = new GuildEM
            {
                Name = nameField.Value.Value
            };

            guildVMF.CreateOrUpdateGuild(guildEM);

            Router.Push(
                pathKey: RouteKeys.Hub.roster,
                loadingKey: LoadingScreenKeys.loading,
                parameters: RouteParams.FirstRoute);
        }
    }
}