using AD.ToolsCollection;
using AD.UI;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class WindowHeaderContainer : MonoBehaviour
    {
        [SerializeField] private UIText titleText;
        [SerializeField] private UIButton closeButton;

        public UIText Title => titleText;
        public ReadOnlyReactiveEvent OnClose => closeButton.OnClick;
    }
}