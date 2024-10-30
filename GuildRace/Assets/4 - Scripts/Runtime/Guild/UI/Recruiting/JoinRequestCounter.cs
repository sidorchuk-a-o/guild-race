using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Components;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class JoinRequestCounter : GameComponent<JoinRequestCounter>
    {
        [SerializeField] private GameObject viewRoot;
        [SerializeField] private UIText counterText;

        private JoinRequestsVM joinRequestsVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            joinRequestsVM = guildVMF.GetJoinRequests();
        }

        protected override void Awake()
        {
            base.Awake();

            viewRoot.SetActive(false);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            joinRequestsVM.AddTo(disp);

            joinRequestsVM
                .ObserveCountChanged()
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(joinRequestsVM.Count);
        }

        private void CountChangedCallback(int count)
        {
            viewRoot.SetActive(count > 0);

            counterText.SetTextParams(count);
        }
    }
}