using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [UnityEditor.CustomEditor(typeof(RewardMechanicsUIParams))]
    public class RewardMechanicsUIParamsEditor : MonoEditor
    {
        private RewardMechanicUIParamsList mechanicParamsList;

        public override void CreateGUI(VisualElement root)
        {
            mechanicParamsList = root.CreateElement<RewardMechanicUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            mechanicParamsList.BindProperty("mechanicParams", data);
        }
    }
}