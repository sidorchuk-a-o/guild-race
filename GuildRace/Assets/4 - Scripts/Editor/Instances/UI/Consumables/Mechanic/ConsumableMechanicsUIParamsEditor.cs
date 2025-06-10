using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [UnityEditor.CustomEditor(typeof(ConsumableMechanicsUIParams))]
    public class ConsumableMechanicsUIParamsEditor : MonoEditor
    {
        private ConsumableMechanicUIParamsList mechanicParamsList;

        public override void CreateGUI(VisualElement root)
        {
            mechanicParamsList = root.CreateElement<ConsumableMechanicUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            mechanicParamsList.BindProperty("mechanicParams", data);
        }
    }
}