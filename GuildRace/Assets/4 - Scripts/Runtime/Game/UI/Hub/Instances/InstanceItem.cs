using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class InstanceItem : MonoBehaviour
    {
        [SerializeField] private UIText nameText;
        [SerializeField] private UIButton button;

        private void Awake()
        {
            button.OnClick
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public async UniTask Init(InstanceVM instanceVM)
        {
            nameText.SetTextParams(instanceVM.NameKey);
        }

        private void ClickCallback()
        {
        }
    }
}