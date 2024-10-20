using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="EquipGeneratorPhaseData"/>
    /// </summary>
    public class EquipGeneratorPhaseBlock
    {
        private PropertyElement minEquipCountField;
        private PropertyElement maxEquipCountField;
        private PropertyElement minEquipLevelField;
        private PropertyElement maxEquipLevelField;

        public void Create(string propertyName, VisualElement root, SerializedData data)
        {
            var pData = data.GetProperty(propertyName);

            root.CreateHeader(pData.DisplayPropertyPath);

            minEquipCountField = root.CreateProperty();
            minEquipCountField.BindProperty("minEquipCount", pData);

            maxEquipCountField = root.CreateProperty();
            maxEquipCountField.BindProperty("maxEquipCount", pData);

            minEquipLevelField = root.CreateProperty();
            minEquipLevelField.BindProperty("minEquipLevel", pData);

            maxEquipLevelField = root.CreateProperty();
            maxEquipLevelField.BindProperty("maxEquipLevel", pData);
        }
    }
}