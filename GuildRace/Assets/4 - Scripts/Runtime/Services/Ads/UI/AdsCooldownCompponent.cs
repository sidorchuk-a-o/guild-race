using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Ads
{
    public class AdsCooldownCompponent : MonoBehaviour
    {
        [SerializeField] private UIText timerText;

        private readonly CompositeDisp timerDisp = new();

        public void Init(TimerVM timerVM, CompositeDisp disp)
        {
            timerDisp.Clear();
            timerDisp.AddTo(disp);

            timerVM.TimeLeftStr
                .Subscribe(x => timerText.SetTextParams(x))
                .AddTo(timerDisp);

            timerVM.TimeLeft
                .Subscribe(x => this.SetActive(x > 0))
                .AddTo(timerDisp);
        }
    }
}