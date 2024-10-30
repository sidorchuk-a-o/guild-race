using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class ClassRoleSelector : MonoBehaviour
    {
        [Header("Role")]
        [SerializeField] private UIText roleNameText;

        [Header("Selector")]
        [SerializeField] private UIButton selectorButton;
        [SerializeField] private string unselectedStateKey = "default";
        [SerializeField] private string selectedStateKey = "selected";

        private ClassRoleSelectorVM classRoleSelectorVM;

        private void Awake()
        {
            selectorButton.OnClick
                .Subscribe(SelectorClickCallback)
                .AddTo(this);
        }

        public void Init(ClassRoleSelectorVM classRoleSelectorVM, CompositeDisp disp)
        {
            this.classRoleSelectorVM = classRoleSelectorVM;

            roleNameText.SetTextParams(classRoleSelectorVM.RoleVM.NameKey);

            classRoleSelectorVM.IsSelected
                .Subscribe(SelectorStateChanged)
                .AddTo(disp);
        }

        private void SelectorStateChanged(bool state)
        {
            selectorButton.SetState(state ? selectedStateKey : unselectedStateKey);
        }

        private void SelectorClickCallback()
        {
            classRoleSelectorVM.SwitchSelectState();
        }
    }
}