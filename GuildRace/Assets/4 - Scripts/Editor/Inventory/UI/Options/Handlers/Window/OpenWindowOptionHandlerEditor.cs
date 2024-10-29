using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="OpenWindowOptionHandler"/>
    /// </summary>
    public abstract class OpenWindowOptionHandlerEditor : OptionHandlerEditor
    {
        private AddressableElement<GameObject> windowRefField;

        protected override void CreateHandlerTab(VisualElement root, SerializedData data)
        {
            base.CreateHandlerTab(root, data);

            root.CreateHeader("Window");

            windowRefField = root.CreateAddressable<GameObject>();
            windowRefField.BindProperty("windowRef", data);
        }
    }
}