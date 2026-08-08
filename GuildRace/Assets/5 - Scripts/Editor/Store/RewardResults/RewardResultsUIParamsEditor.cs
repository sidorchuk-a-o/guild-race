using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Store
{
    [CustomEditor(typeof(RewardResultsUIParams))]
    public class RewardResultsUIParamsEditor : MonoEditor
    {
        private RewardResultUIParamsList resultParamsList;

        public override void CreateGUI(VisualElement root)
        {
            resultParamsList = root.CreateElement<RewardResultUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            resultParamsList.BindProperty("resultParams", data);
        }
    }
}