using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    public class GuildConfigEditor : ConfigEditor<GuildConfig>
    {
        private PropertyElement maxCharactersCountField;
        private GuildRanksList defaultGuildRanksList;

        private CharactersParamsEditor charactersParamsEditor;
        private RecruitingParamsEditor recruitingParamsEditor;
        private GuildBankParamsEditor guildBankParamsEditor;
        private EmblemParamsEditor emblemParamsEditor;
        private PropertyElement guildPowerKeyField;
        private PropertyElement sendTimeField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTabs("Guild", CreateGuildTabs);
            tabs.CreateTabs("Characters", CreateCharactersTabs);
            tabs.CreateTabs("Recruiting", CreateRecruitingTabs);
            tabs.CreateTabs("Guild Bank", CreateGuildBankTabs);
            tabs.CreateTabs("Emblem", CreateEmblemTabs);
        }

        private void CreateGuildTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Common", CreateGuildTab);
            tabs.CreateTab("Ranks", CreateRanksTab);
        }

        private void CreateGuildTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Common");

            maxCharactersCountField = root.CreateProperty();
            maxCharactersCountField.BindProperty("maxCharactersCount", data);

            root.CreateHeader("Leaderboard Params");

            var leaderboardData = data.GetProperty("leaderboardParams");

            guildPowerKeyField = root.CreateProperty();
            guildPowerKeyField.BindProperty("guildPowerKey", leaderboardData);

            sendTimeField = root.CreateProperty();
            sendTimeField.BindProperty("sendTime", leaderboardData);
        }

        private void CreateRanksTab(VisualElement root, SerializedData data)
        {
            defaultGuildRanksList = root.CreateElement<GuildRanksList>();
            defaultGuildRanksList.BindProperty("defaultGuildRanks", data);
        }

        private void CreateCharactersTabs(TabsContainer tabs)
        {
            charactersParamsEditor = new();
            charactersParamsEditor.CreateTabs(tabs);
        }

        private void CreateRecruitingTabs(TabsContainer tabs)
        {
            recruitingParamsEditor = new();
            recruitingParamsEditor.CreateTabs(tabs);
        }

        private void CreateGuildBankTabs(TabsContainer tabs)
        {
            guildBankParamsEditor = new();
            guildBankParamsEditor.CreateTabs(tabs);
        }

        private void CreateEmblemTabs(TabsContainer tabs)
        {
            emblemParamsEditor = new();
            emblemParamsEditor.CreateTabs(tabs);
        }
    }
}