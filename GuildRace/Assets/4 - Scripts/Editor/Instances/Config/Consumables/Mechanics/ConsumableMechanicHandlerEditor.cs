using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="ConsumableMechanicHandler"/>
    /// </summary>
    public abstract class ConsumableMechanicHandlerEditor : EntityEditor
    {
        public abstract void CreateParamsEditor(VisualElement root, List<string> questParams);
    }
}