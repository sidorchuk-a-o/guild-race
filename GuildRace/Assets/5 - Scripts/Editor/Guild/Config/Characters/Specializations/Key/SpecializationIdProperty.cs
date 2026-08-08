using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Guild
{
    [CustomPropertyDrawer(typeof(SpecializationId))]
    public class SpecializationIdProperty : KeyPropertyDrawer<SpecializationId, int>
    {
    }

    [KeyEditor(typeof(SpecializationId))]
    public class SpecializationIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateSpecializationsViewCollection;
        }
    }
}