using AD.Services.Leaderboards;
using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Leaderboards
{
    public class ScoreChangedContainer : MonoBehaviour
    {
        private const float showTime = 2;

        [SerializeField] private UIText valueText;
        [Space]
        [SerializeField] private UIStates visibleState;
        [SerializeField] private string hiddenKey = "default";
        [SerializeField] private string showedKey = "showed";
        [Space]
        [SerializeField] private UIStates valueState;
        [SerializeField] private string positiveKey = "positive";
        [SerializeField] private string negativeKey = "negative";

        private int delta;
        private float timer;
        private bool? lastIsPositive;

        public void Init(LeaderboardVM leaderboardVM, CompositeDisp disp)
        {
            delta = 0;
            lastIsPositive = null;
            visibleState.SetState(hiddenKey);

            leaderboardVM.Delta
                .SilentSubscribe(DeltaChangedCallback)
                .AddTo(disp);
        }

        private void DeltaChangedCallback(int value)
        {
            var isPositive = value > 0;

            if (lastIsPositive.HasValue && lastIsPositive != isPositive)
            {
                delta = 0;
            }

            delta += value;
            lastIsPositive = value > 0;

            var sign = isPositive ? "+" : string.Empty;

            var valueStr = $"{sign}{delta}";
            var valueStateKey = isPositive ? positiveKey : negativeKey;

            valueText.SetTextParams(valueStr);

            valueState.SetState(valueStateKey);
            visibleState.SetState(showedKey);

            timer = showTime;
            enabled = true;
        }

        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            delta = 0;
            lastIsPositive = null;

            visibleState.SetState(hiddenKey);

            enabled = false;
        }
    }
}