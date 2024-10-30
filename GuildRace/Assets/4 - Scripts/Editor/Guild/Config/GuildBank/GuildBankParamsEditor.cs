using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="GuildBankParams"/>
    /// </summary>
    public class GuildBankParamsEditor
    {
        private GuildBankTabsList tabsList;

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
            tabsList = root.CreateElement<GuildBankTabsList>();
            tabsList.BindProperty("tabs", GetData(data));
            tabsList.headerTitle = "Storage Pages";
        }
    }
}