using AD.ToolsCollection;
using Game.Components;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class JoinRequestCounter : GameComponent<JoinRequestCounter>
    {
        [SerializeField] private CounterComponent counter;

        private JoinRequestsVM joinRequestsVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            joinRequestsVM = guildVMF.GetJoinRequests();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            joinRequestsVM.AddTo(disp);

            counter.Init(joinRequestsVM.ObserveCountChanged(), joinRequestsVM.Count, disp);
        }
    }
}