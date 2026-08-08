using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    public class GuildRankItem : ListItemElement<GuildRankData>
    {
        private LocalizeKeyElement defaultNameKeyField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            defaultNameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            defaultNameKeyField.FlexGrow(1);
            defaultNameKeyField.LabelOn = false;
            defaultNameKeyField.RemoveOn = false;
            defaultNameKeyField.PreviewOn = true;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            defaultNameKeyField.BindProperty("defaultNameKey", data);

            TitleLabel.Display(false);
        }
    }
}