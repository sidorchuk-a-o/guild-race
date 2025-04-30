using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="EmblemParams"/>
    /// </summary>
    public class EmblemParamsEditor
    {
        private SymbolsList symbolsList;
        private DivisionsList divisionsList;
        private PropertyElement colorsList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("emblemParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Symbols", CreateSymbolsTab);
            tabs.CreateTab("Divisions", CreateDivisionsTab);
            tabs.CreateTab("Colors", CreateColorsTab);
        }

        private void CreateSymbolsTab(VisualElement root, SerializedData data)
        {
            symbolsList = root.CreateElement<SymbolsList>();
            symbolsList.BindProperty("symbols", GetData(data));
        }

        private void CreateDivisionsTab(VisualElement root, SerializedData data)
        {
            divisionsList = root.CreateElement<DivisionsList>();
            divisionsList.BindProperty("divisions", GetData(data));
        }

        private void CreateColorsTab(VisualElement root, SerializedData data)
        {
            colorsList = root.CreateProperty();
            colorsList.BindProperty("colors", GetData(data));
        }
    }
}