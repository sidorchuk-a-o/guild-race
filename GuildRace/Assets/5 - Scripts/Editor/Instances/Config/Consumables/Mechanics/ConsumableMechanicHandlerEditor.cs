using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    /// <summary>
    ///     Editor: <see cref="ConsumableMechanicHandler" />
    /// </summary>
    public abstract class ConsumableMechanicHandlerEditor : DataEditor
    {
        public abstract void CreateParamsEditor(VisualElement root, List<string> questParams);
    }
}