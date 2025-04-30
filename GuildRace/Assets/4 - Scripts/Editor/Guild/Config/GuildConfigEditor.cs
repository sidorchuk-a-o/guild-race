using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [ConfigEditor(typeof(GuildConfig))]
    public class GuildConfigEditor : ConfigEditor
    {
        private PropertyElement maxCharactersCountField;
        private GuildRanksList defaultGuildRanksList;

        private CharactersParamsEditor charactersParamsEditor;
        private RecruitingParamsEditor recruitingParamsEditor;
        private GuildBankParamsEditor guildBankParamsEditor;
        private EmblemParamsEditor emblemParamsEditor;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Guild", CreateGuildTab);
            tabs.CreateTabs("Characters", CreateCharactersTabs);
            tabs.CreateTabs("Recruiting", CreateRecruitingTabs);
            tabs.CreateTabs("Guild Bank", CreateGuildBankTabs);
            tabs.CreateTabs("Emblem", CreateEmblemTabs);
        }

        private void CreateGuildTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Common");

            maxCharactersCountField = root.CreateProperty();
            maxCharactersCountField.BindProperty("maxCharactersCount", data);

            root.CreateHeader("Guild Ranks (Default)");

            defaultGuildRanksList = root.CreateElement<GuildRanksList>();
            defaultGuildRanksList.BindProperty("defaultGuildRanks", data);
        }

        private void CreateCharactersTabs(TabsContainer tabs)
        {
            charactersParamsEditor = new CharactersParamsEditor();
            charactersParamsEditor.CreateTabs(tabs);
        }

        private void CreateRecruitingTabs(TabsContainer tabs)
        {
            recruitingParamsEditor = new RecruitingParamsEditor();
            recruitingParamsEditor.CreateTabs(tabs);
        }

        private void CreateGuildBankTabs(TabsContainer tabs)
        {
            guildBankParamsEditor = new GuildBankParamsEditor();
            guildBankParamsEditor.CreateTabs(tabs);
        }

        private void CreateEmblemTabs(TabsContainer tabs)
        {
            emblemParamsEditor = new EmblemParamsEditor();
            emblemParamsEditor.CreateTabs(tabs);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Guild")]
        public static GuildConfigEditor GoToEditor()
        {
            return GoToEditor<GuildConfigEditor>(width: 500);
        }

        public static GuildConfigEditor GoToEditor(GuildConfig config)
        {
            return GoToEditor<GuildConfigEditor>(config, width: 500);
        }
    }
}