using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Input;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Inventory
{
    public abstract class StackableItemDialog : BaseDialog
    {
        [Header("Size")]
        [SerializeField] private Slider sizeSlider;
        [SerializeField] private UIInputField sizeInput;

        public const string contextKey = "context";

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

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            parameters.TryGetRouteValue<StackableContext>(contextKey, out var context);

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
            var stackableItemVM = selectedItemVM as IStackableItemVM;

            return stackableItemVM.StackVM.Value - 1;
        }

        private void InitPosition()
        {
            var windowRT = transform as RectTransform;
            var windowParent = transform.parent as RectTransform;

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
            var value = sizeInput.Value.Value.IntParse();
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