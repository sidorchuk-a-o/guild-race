using AD.ToolsCollection;
using AD.UI;
using UnityEngine;

namespace Game.Inventory
{
    public class WindowHeaderContainer : MonoBehaviour
    {
        [SerializeField] private UIText titleText;
        [SerializeField] private UIButton closeButton;

        public UIText Title => titleText;
        public IObservable OnClose => closeButton.OnClick;
    }
}