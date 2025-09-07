using AD.Services.Audio;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class PickedItemPreview : MonoBehaviour
    {
        [SerializeField] private ItemIconImage iconImage;
        [SerializeField] private AudioKey showItemKey;

        private readonly CompositeDisp disp = new();

        private IAudioService audioService;

        [Inject]
        public void Inject(IAudioService audioService)
        {
            this.audioService = audioService;
        }

        private void OnEnable()
        {
            disp.AddTo(this);
        }

        private void OnDisable()
        {
            disp.Clear();
        }

        public void ShowItem(ItemVM itemVM, bool iconStaticOn, int cellSize)
        {
            if (iconStaticOn)
            {
                transform.ApplyItemBounds(itemVM.BoundsVM.Value, cellSize);
            }
            else
            {
                itemVM.BoundsVM.ObserveValue()
                    .Subscribe(x => transform.ApplyItemSize(x.size, cellSize))
                    .AddTo(disp);
            }

            iconImage.UpdateIcon(itemVM, iconStaticOn, disp);

            audioService?.UiModule.TryPlaySound(showItemKey);

            this.SetActive(true);
        }
    }
}