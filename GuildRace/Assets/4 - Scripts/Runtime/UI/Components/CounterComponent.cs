using System;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class CounterComponent : MonoBehaviour
    {
        [SerializeField] private GameObject viewRoot;
        [SerializeField] private UIText counterText;

        private void Awake()
        {
            viewRoot.SetActive(false);
        }

        public void Init(IReadOnlyReactiveProperty<int> property, CompositeDisp disp)
        {
            property
                .SilentSubscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(property.Value);
        }

        public void Init(IObservable<int> property, int count, CompositeDisp disp)
        {
            property
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(count);
        }

        private void CountChangedCallback(int count)
        {
            viewRoot.SetActive(count > 0);

            counterText.SetTextParams(count);
        }
    }
}