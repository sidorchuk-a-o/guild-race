using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Editor: <see cref="RecyclingParams"/>
    /// </summary>
    public class RecyclingParamsEditor
    {
        private RecycleSlotElement recycleSlotEditor;
        private RecyclingRarityModsList rarityModsList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("recyclingParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.FlexWidth(30);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Recycle Slot - Created");

            recycleSlotEditor = root.CreateElement<RecycleSlotElement>();
            recycleSlotEditor.BindProperty("recycleSlot", GetData(data));

            root.CreateHeader("Rarity Mods");

            rarityModsList = root.CreateElement<RecyclingRarityModsList>();
            rarityModsList.BindProperty("rarityMods", GetData(data));
        }
    }
}