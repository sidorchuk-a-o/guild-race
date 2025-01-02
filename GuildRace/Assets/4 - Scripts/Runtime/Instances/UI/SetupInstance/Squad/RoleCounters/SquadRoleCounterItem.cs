using AD.ToolsCollection;
using AD.UI;
using Game.Guild;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class SquadRoleCounterItem : MonoBehaviour
    {
        [SerializeField] private RoleId role;
        [Space]
        [SerializeField] private Image roleIconImage;
        [SerializeField] private UIText countText;

        public RoleId Role => role;

        public void Init(SquadRolesCounterVM counterVM, CompositeDisp disp)
        {
            counterVM.CountStr
                .Subscribe(x => countText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}