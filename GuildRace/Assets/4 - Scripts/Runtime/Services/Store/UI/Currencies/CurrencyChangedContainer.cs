using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Store
{
    public class CurrencyChangedContainer : MonoBehaviour
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

        private long delta;
        private float timer;
        private bool? lastIsPositive;

        private void Awake()
        {
            enabled = false;
        }

        public void Init(CurrencyVM currencyVM, CompositeDisp disp)
        {
            delta = 0;
            lastIsPositive = null;

            currencyVM.Delta
                .SilentSubscribe(DeltaChangedCallback)
                .AddTo(disp);
        }

        private void DeltaChangedCallback(long value)
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