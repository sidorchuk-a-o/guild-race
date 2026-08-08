using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.UI
{
    public class CounterComponent : MonoBehaviour
    {
        [SerializeField] private GameObject viewRoot;
        [SerializeField] private UIText counterText;

        private void Awake()
        {
            viewRoot.SetActive(false);
        }

        public void Init(ReadOnlyReactiveProperty<int> property, CompositeDisp disp)
        {
            property
                .SilentSubscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(property.CurrentValue);
        }

        public void Init(Observable<int> property, int count, CompositeDisp disp)
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