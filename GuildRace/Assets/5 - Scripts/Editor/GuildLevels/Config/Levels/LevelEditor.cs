using AD.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.GuildLevels
{
    [DataEditor(typeof(LevelData))]
    public class LevelEditor : DataEditor
    {
        private PropertyElement descKeyField;
        private CurrencyAmountElement unlockPriceField;
        private LevelMechanicBlock mechanicBlock;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

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