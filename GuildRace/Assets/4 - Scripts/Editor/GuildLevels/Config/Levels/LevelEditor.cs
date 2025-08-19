using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.GuildLevels
{
    [GuildLevelsEditor(typeof(LevelData))]
    public class LevelEditor : EntityEditor
    {
        private PropertyElement descKeyField;
        private CurrencyAmountElement unlockPriceField;
        private LevelMechanicBlock mechanicBlock;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            root.CreateHeader("Price");

            unlockPriceField = root.CreateElement<CurrencyAmountElement>();
            unlockPriceField.BindProperty("unlockPrice", data);

            mechanicBlock = root.CreateElement<LevelMechanicBlock>();
            mechanicBlock.BindProperty("mechanic", data);
        }
    }
}