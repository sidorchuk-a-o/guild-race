using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(Rarity))]
    public class RarityProperty : KeyPropertyDrawer<Rarity, int>
    {
    }

    [KeyEditor(typeof(Rarity))]
    public class RarityElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetRaritiesCollection;
        }
    }
}