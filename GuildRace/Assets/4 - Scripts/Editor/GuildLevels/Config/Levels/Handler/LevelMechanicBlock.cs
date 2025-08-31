using System;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.GuildLevels
{
    /// <summary>
    /// Block: <see cref="LevelMechanic"/>
    /// </summary>
    public class LevelMechanicBlock : Element
    {
        private VisualElement drawBlock;
        private VisualElement createBlock;

        private PopupElement<Type> typesCollectionPopup;
        private LevelMechanicEditor mechanicElement;
        private VisualElement mechanicElementBlock;
        private LabelElement headerLabel;

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Mechanic Block");
            root.CreateSpace();

            drawBlock = root.CreateContainer();
            createBlock = root.CreateContainer();

            CreateCreateBlock(createBlock);
            CreateDrawBlock(drawBlock);
        }

        private void CreateCreateBlock(VisualElement root)
        {
            root.ConvertToRow();

            var typesCollection = GuildLevelsEditorState.GetGuildLevelMechanicsCollection();

            typesCollectionPopup = root.CreatePopup(typesCollection, title: "Mechanic Type");

            root.CreateSmallButton(
                icon: DrawEditorUtils.CreateIcon,
                clickCallback: CreateMechanicCallback);
        }

        private void CreateDrawBlock(VisualElement root)
        {
            // header
            var headerBlock = root.CreateContainer();

            headerBlock.ConvertToRow();

            headerLabel = headerBlock.CreateElement<LabelElement>();
            headerLabel.FlexGrow(1).FontSize(16).FontStyle(FontStyle.Bold);

            headerBlock.CreateSmallButton(
                icon: DrawEditorUtils.RemoveIcon,
                clickCallback: RemoveMechanicCallback);

            // mechanic
            mechanicElementBlock = root.CreateContainer();
        }

        private void CreateMechanicCallback()
        {
            var newData = DataFactory.Create(typesCollectionPopup.value);
            var saveMeta = new SaveMeta(isSubObject: true, data);

            DataFactory.Save(newData, saveMeta);

            data.SetValue(newData);

            UpdateBlocksState();
        }

        private void RemoveMechanicCallback()
        {
            var value = data.GetValue();
            var saveMeta = new SaveMeta(isSubObject: true, data);

            DataFactory.Remove(value, saveMeta);

            data.SetValue(null);

            UpdateBlocksState();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            UpdateBlocksState();
        }

        private void UpdateBlocksState()
        {
            var value = data.GetValue();

            drawBlock.Display(value != null);
            createBlock.Display(value == null);

            CreateMechanicEditor();
        }

        private void CreateMechanicEditor()
        {
            var value = data.GetValue();

            mechanicElementBlock.Clear();

            if (value == null)
            {
                return;
            }

            var factory = GuildLevelsEditorState.LevelMechanicsFactory;
            var editorData = factory.GetEditorData(data.DataType);

            if (editorData == null)
            {
                headerLabel.text = data.DataType.Name;
                return;
            }

            headerLabel.text = editorData.Title;

            mechanicElement = factory.CreateEditor<LevelMechanicEditor>(data.DataType);
            mechanicElement.CreateGUI(mechanicElementBlock);
            mechanicElement.BindData(data);
        }
    }
}