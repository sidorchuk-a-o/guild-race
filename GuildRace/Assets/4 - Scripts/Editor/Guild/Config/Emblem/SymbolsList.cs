using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class SymbolsList : AddressableList<Sprite>
    {
        public override void BindData(SerializedData data)
        {
            UpdateSprites(data);

            base.BindData(data);
        }

        private static void UpdateSprites(SerializedData data)
        {
            var assets = AssetsEditorUtils.LoadAssetsByName<Sprite>("symbol-");
            var values = assets.Select(x => x.CreateAssetRef()).ToList();

            data.SetValue(values);
            data.MarkAsDirty();
        }
    }
}