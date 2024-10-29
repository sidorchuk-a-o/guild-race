﻿using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemsParams"/>
    /// </summary>
    public class ItemsParamsEditor
    {
        private RaritiesList raritiesList;
        private ItemsFactoriesList factoriesList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("itemsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            raritiesList = root.CreateElement<RaritiesList>();
            raritiesList.BindProperty("rarities", GetData(data));
            raritiesList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);

            factoriesList = root.CreateElement<ItemsFactoriesList>();
            factoriesList.BindProperty("factories", GetData(data));
            factoriesList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);
        }
    }
}