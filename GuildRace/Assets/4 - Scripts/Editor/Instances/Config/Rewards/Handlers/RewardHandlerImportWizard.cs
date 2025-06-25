using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(RewardHandler))]
    public class RewardHandlerImportWizard : EntitiesImportWizard<int>
    {
        private List<Type> subclasses;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "loot-data";
        public override string SheetRange => "A2:D";

        protected override Type GetDataType(IDataRow row)
        {
            subclasses ??= DataType.GetSubclasses().ToList();

            var typeName = row["Type"];
            var dataType = subclasses.FirstOrDefault(x => x.Name.Contains(typeName));

            return dataType;
        }

        protected override async void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var editorsFactory = InstancesEditorState.EditorsFactory;
            var handlerEditor = editorsFactory.CreateEditor<RewardHandlerEditor>(data.DataType);

            LockWizard();

            await handlerEditor.ParseAndApplyParams(data, row["Params"], SheetId);

            UnlockWizard();
        }
    }
}