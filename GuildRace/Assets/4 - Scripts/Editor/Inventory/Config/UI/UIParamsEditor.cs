using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="UIParams"/>
    /// </summary>
    public class UIParamsEditor
    {
        private PropertyElement cellSizeField;

        private PickupHandlersList pickupHandlersList;
        private ReleaseHandlersList placeHandlersList;
        private ReleaseHandlersList splitHandlersList;
        private ReleaseHandlersList rollbackHandlersList;
        private OptionHandlersList optionHandlersList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("uiParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Drag & Drop", CreateDragAndDropTab);
            tabs.CreateTab("Options", CreateOptionsTab);
            tabs.CreateTab("Grid", CreateGridTab);
        }

        private void CreateDragAndDropTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            pickupHandlersList = root.CreateElement<PickupHandlersList>();
            pickupHandlersList.headerTitle = "Pickup Handlers";
            pickupHandlersList.BindProperty("pickupHandlers", GetData(data));
            pickupHandlersList.FlexWidth(25).MarginRight(10);

            placeHandlersList = root.CreateElement<ReleaseHandlersList>();
            placeHandlersList.headerTitle = "Place Handlers";
            placeHandlersList.BindProperty("placeHandlers", GetData(data));
            placeHandlersList.FlexWidth(25).MarginRight(10);

            splitHandlersList = root.CreateElement<ReleaseHandlersList>();
            splitHandlersList.headerTitle = "Split Handlers";
            splitHandlersList.BindProperty("splitHandlers", GetData(data));
            splitHandlersList.FlexWidth(25).MarginRight(10);

            rollbackHandlersList = root.CreateElement<ReleaseHandlersList>();
            rollbackHandlersList.headerTitle = "Rollback Handlers";
            rollbackHandlersList.BindProperty("rollbackHandlers", GetData(data));
            rollbackHandlersList.FlexWidth(25);
        }

        private void CreateOptionsTab(VisualElement root, SerializedData data)
        {
            optionHandlersList = root.CreateElement<OptionHandlersList>();
            optionHandlersList.BindProperty("optionHandlers", GetData(data));
            optionHandlersList.FlexWidth(33).MarginRight(10);
        }

        private void CreateGridTab(VisualElement root, SerializedData data)
        {
            root.ConvertToColumn();

            cellSizeField = root.CreateProperty();
            cellSizeField.BindProperty("cellSize", GetData(data));
            cellSizeField.MaxWidth(25, LengthUnit.Percent).MarginRight(10);
        }
    }
}