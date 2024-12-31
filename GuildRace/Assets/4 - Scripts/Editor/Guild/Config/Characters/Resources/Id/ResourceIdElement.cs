using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(ResourceId))]
    public class ResourceIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateResourcesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}