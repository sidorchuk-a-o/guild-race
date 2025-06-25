using System.Collections.Generic;
using System.Threading.Tasks;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="RewardHandler"/>
    /// </summary>
    public abstract class RewardHandlerEditor : EntityEditor
    {
        public abstract void CreateParamsEditor(VisualElement root, List<string> rewardParams);
        public abstract Task ParseAndApplyParams(SerializedData data, string sheetParams, string sheetId);
    }
}