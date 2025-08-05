using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Store
{
    [UnityEditor.CustomEditor(typeof(RewardResultsUIParams))]
    public class RewardResultsUIParamsEditor : MonoEditor
    {
        private RewardResultUIParamsList resultParamsList;

        public override void CreateGUI(VisualElement root)
        {
            resultParamsList = root.CreateElement<RewardResultUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            resultParamsList.BindProperty("resultParams", data);
        }
    }
}