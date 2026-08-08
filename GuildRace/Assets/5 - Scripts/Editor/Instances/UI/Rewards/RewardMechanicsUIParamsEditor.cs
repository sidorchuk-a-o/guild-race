using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [CustomEditor(typeof(RewardMechanicsUIParams))]
    public class RewardMechanicsUIParamsEditor : MonoEditor
    {
        private RewardMechanicUIParamsList mechanicParamsList;

        public override void CreateGUI(VisualElement root)
        {
            mechanicParamsList = root.CreateElement<RewardMechanicUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            mechanicParamsList.BindProperty("mechanicParams", data);
        }
    }
}