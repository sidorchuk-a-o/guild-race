using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class CharacterInstanceItem : MonoBehaviour
    {
        [SerializeField] private GameObject instanceContainer;
        [Space]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color otherInstanceColor;
        [SerializeField] private Color currentInstanceColor;

        private ActiveInstanceVM activeInstanceVM;

        private readonly CompositeDisp disp = new();

        private void OnDisable()
        {
            disp.Clear();
        }

        public void Init(CharacterVM characterVM, ActiveInstanceVM activeInstanceVM)
        {
            this.activeInstanceVM = activeInstanceVM;

            disp.Clear();
            disp.AddTo(this);

            characterVM.InstanceVM
                .Subscribe(InstanceChangedCallback)
                .AddTo(disp);
        }

        private void InstanceChangedCallback(ActiveInstanceVM instanceVM)
        {
            var hasInstance = instanceVM != null;
            var sameInstance = hasInstance && instanceVM.Id == activeInstanceVM.Id;

            instanceContainer.SetActive(hasInstance);

            backgroundImage.color = sameInstance
                ? currentInstanceColor
                : otherInstanceColor;
        }
    }
}