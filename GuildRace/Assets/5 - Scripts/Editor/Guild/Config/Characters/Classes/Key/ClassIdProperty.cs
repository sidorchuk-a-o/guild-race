using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Guild
{
    [CustomPropertyDrawer(typeof(ClassId))]
    public class ClassIdProperty : KeyPropertyDrawer<ClassId, int>
    {
    }

    [KeyEditor(typeof(ClassId))]
    public class ClassIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateClassesViewCollection;
        }
    }
}