using AD.ToolsCollection;

namespace Game.Guild
{
    public class ClassesList : ListElement<ClassData, ClassItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(ClassCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}