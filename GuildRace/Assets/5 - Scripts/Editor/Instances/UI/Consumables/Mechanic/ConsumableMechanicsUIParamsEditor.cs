using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [CustomEditor(typeof(ConsumableMechanicsUIParams))]
    public class ConsumableMechanicsUIParamsEditor : MonoEditor
    {
        private ConsumableMechanicUIParamsList mechanicParamsList;

        public override void CreateGUI(VisualElement root)
        {
            mechanicParamsList = root.CreateElement<ConsumableMechanicUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            mechanicParamsList.BindProperty("mechanicParams", data);
        }
    }
}