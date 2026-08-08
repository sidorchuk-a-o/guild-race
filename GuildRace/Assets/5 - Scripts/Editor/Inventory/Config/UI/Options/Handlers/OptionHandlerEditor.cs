using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    /// <summary>
    ///     Editor: <see cref="OptionHandler" />
    /// </summary>
    public abstract class OptionHandlerEditor : DataEditor
    {
        private KeyElement<string> titleKeyField;
        private AddressableElement<Sprite> iconRefField;
        private AddressableElement<GameObject> buttonRefField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab(CreateHandlerTab);
        }

        protected virtual void CreateHandlerTab(VisualElement root, SerializedData data)
        {
            titleKeyField = root.CreateKeyPopup<LocalizeKey, string>();
            titleKeyField.BindProperty("titleKey", data);

            root.CreateSpace();

            iconRefField = root.CreateAddressable<Sprite>();
            iconRefField.BindProperty("iconRef", data);

            buttonRefField = root.CreateAddressable<GameObject>();
            buttonRefField.BindProperty("buttonRef", data);
        }
    }
}