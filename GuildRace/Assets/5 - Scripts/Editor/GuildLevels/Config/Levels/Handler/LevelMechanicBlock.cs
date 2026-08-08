using System;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.GuildLevels
{
    /// <summary>
    ///     Block: <see cref="LevelMechanic" />
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

            typesCollectionPopup = root.CreatePopup("Mechanic Type", typesCollection);

            root.CreateSmallButton(DrawEditorUtils.CreateIcon, CreateMechanicCallback);
        }

        private void CreateDrawBlock(VisualElement root)
        {
            // header
            var headerBlock = root.CreateContainer();

            headerBlock.ConvertToRow();

            headerLabel = headerBlock.CreateElement<LabelElement>();
            headerLabel.FlexGrow(1).FontSize(16).FontStyle(FontStyle.Bold);

            headerBlock.CreateSmallButton(DrawEditorUtils.RemoveIcon, RemoveMechanicCallback);

            // mechanic
            mechanicElementBlock = root.CreateContainer();
        }

        private void CreateMechanicCallback()
        {
            var newData = DataFactory.CreateInstance(typesCollectionPopup.Value);
            var saveMeta = new SaveMeta(isSubObject: true, elementData);

            DataFactory.SaveInstance(newData, saveMeta);

            elementData.SetValue(newData);

            UpdateBlocksState();
        }

        private void RemoveMechanicCallback()
        {
            var value = elementData.GetValue();
            var saveMeta = new SaveMeta(isSubObject: true, elementData);

            DataFactory.RemoveInstance(value, saveMeta);

            elementData.SetValue(null);

            UpdateBlocksState();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            UpdateBlocksState();
        }

        private void UpdateBlocksState()
        {
            var value = elementData.GetValue();

            drawBlock.Display(value != null);
            createBlock.Display(value == null);

            CreateMechanicEditor();
        }

        private void CreateMechanicEditor()
        {
            var value = elementData.GetValue();

            mechanicElementBlock.Clear();

            if (value == null)
            {
                return;
            }

            var factory = DataEditorsFactory.Instance;
            var editorData = factory.GetEditorData(elementData.DataType);

            if (editorData == null)
            {
                headerLabel.Text = elementData.DataType.Name;
                return;
            }

            headerLabel.Text = editorData.Title;

            mechanicElement = factory.CreateEditor<LevelMechanicEditor>(elementData.DataType);
            mechanicElement.CreateGUI(mechanicElementBlock);
            mechanicElement.BindData(elementData);
        }
    }
}