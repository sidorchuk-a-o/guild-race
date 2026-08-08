using System.Threading;
using AD.Game.Input;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Inventory
{
    public abstract class StackableItemDialog : BaseDialog
    {
        public const string ContextKey = "context";

        [Header("Size")]
        [SerializeField] private Slider sizeSlider;
        [SerializeField] private UIInputField sizeInput;

        private IInventoryInputModule inventoryInputs;

        private int maxSize;
        private int maxSlider;
        private float stepSize;

        protected InventoryVMFactory InventoryVMF { get; private set; }
        protected StackableContext Context { get; private set; }
        protected int Size { get; private set; }

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF, IInputService inputService)
        {
            InventoryVMF = inventoryVMF;

            inventoryInputs = inputService.InventoryModule;
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            parameters.TryGetRouteValue<StackableContext>(ContextKey, out var context);

            Context = context;

            // slider
            maxSize = GetMaxSize();
            maxSlider = Mathf.Min(maxSize, 21);
            stepSize = maxSize / (float)maxSlider;

            sizeSlider.minValue = 1;
            sizeSlider.maxValue = maxSlider;
            sizeSlider.value = Mathf.CeilToInt(maxSlider / 2f);

            Size = Mathf.RoundToInt(sizeSlider.value * stepSize);

            sizeInput.SetValue(Size);

            // subscribes
            sizeSlider.onValueChanged
                .AsObservable()
                .Subscribe(SliderValueChanged)
                .AddTo(disp);

            sizeInput.Value
                .SilentSubscribe(InputValueChanged)
                .AddTo(disp);

            // position
            InitPosition();
        }

        protected virtual int GetMaxSize()
        {
            var selectedItemVM = Context.SelectedItemVM;
            var stackableItemVM = (IStackableItemVM)selectedItemVM;

            return stackableItemVM.StackVM.Value - 1;
        }

        private void InitPosition()
        {
            var windowRT = (RectTransform)transform;
            var windowParent = (RectTransform)transform.parent;

            var cursorPosition = inventoryInputs.CursorPosition;
            var windowPosition = windowParent.GetLocalPosition(cursorPosition);

            windowRT.anchoredPosition = windowPosition;
        }

        // == Callbacks ==

        protected override void OkCallback()
        {
            Context.CompleteTask.TrySetResult();

            base.OkCallback();
        }

        protected override void CancelCallback()
        {
            Context.CompleteTask.TrySetResult();

            base.CancelCallback();
        }

        private void SliderValueChanged()
        {
            var size = Mathf.RoundToInt(sizeSlider.value * stepSize);
            var clampedValue = Mathf.Clamp(size, 1, maxSize);

            sizeInput.SetValue(clampedValue);
        }

        private void InputValueChanged()
        {
            var value = sizeInput.Value.CurrentValue.IntParse();
            var clampedValue = Mathf.Clamp(value, 1, maxSize);

            if (value != clampedValue)
            {
                sizeInput.SetValue(clampedValue);
                return;
            }

            Size = value;

            // upd slider
            var sliderValue = Mathf.RoundToInt(Size / stepSize);

            sizeSlider.SetValueWithoutNotify(sliderValue);
        }
    }
}