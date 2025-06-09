using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class ClassRoleSelector : MonoBehaviour
    {
        [Header("Role")]
        [SerializeField] private Image roleIconImage;
        [SerializeField] private UIText roleNameText;

        [Header("Selector")]
        [SerializeField] private UIButton selectorButton;
        [SerializeField] private string unselectedStateKey = "default";
        [SerializeField] private string selectedStateKey = "selected";

        private ClassRoleSelectorVM selectorVM;

        private void Awake()
        {
            selectorButton.OnClick
                .Subscribe(SelectorClickCallback)
                .AddTo(this);
        }

        public async UniTask Init(ClassRoleSelectorVM selectorVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.selectorVM = selectorVM;

            var roleIcon = await selectorVM.RoleVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            roleIconImage.sprite = roleIcon;
            roleNameText.SetTextParams(selectorVM.RoleVM.NameKey);

            selectorVM.IsSelected
                .Subscribe(SelectorStateChanged)
                .AddTo(disp);
        }

        private void SelectorStateChanged(bool state)
        {
            selectorButton.SetState(state ? selectedStateKey : unselectedStateKey);
        }

        private void SelectorClickCallback()
        {
            selectorVM.SwitchSelectState();
        }
    }
}