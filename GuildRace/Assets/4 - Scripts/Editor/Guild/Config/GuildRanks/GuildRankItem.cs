using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="GuildRankData"/>
    /// </summary>
    public class GuildRankItem : ListItemElement
    {
        private LocalizeKeyElement defaultNameKeyField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            defaultNameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            defaultNameKeyField.FlexGrow(1);
            defaultNameKeyField.labelOn = false;
            defaultNameKeyField.removeOn = false;
            defaultNameKeyField.previewOn = true;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            defaultNameKeyField.BindProperty("defaultNameKey", data);
        }
    }
}