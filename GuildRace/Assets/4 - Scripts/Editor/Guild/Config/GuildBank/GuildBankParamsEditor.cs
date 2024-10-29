using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="GuildBankParams"/>
    /// </summary>
    public class GuildBankParamsEditor
    {
        private ItemsGridsList storageGridsList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("guildBankParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Storage", CreateStorageTab);
        }

        private void CreateStorageTab(VisualElement root, SerializedData data)
        {
            storageGridsList = root.CreateElement<ItemsGridsList>();
            storageGridsList.BindProperty("storagePages", GetData(data));
            storageGridsList.headerTitle = "Storage Pages";
        }
    }
}