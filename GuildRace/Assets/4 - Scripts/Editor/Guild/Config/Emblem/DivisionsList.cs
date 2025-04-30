using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Guild
{
    public class DivisionsList : AddressableList<Material>
    {
        public override void BindData(SerializedData data)
        {
            UpdateMaterials(data);

            base.BindData(data);
        }

        private static void UpdateMaterials(SerializedData data)
        {
            var assets = AssetsEditorUtils.LoadAssetsByName<Material>("Shield-");
            var values = assets.Select(x => x.CreateAssetRef()).ToList();

            data.SetValue(values);
            data.MarkAsDirty();
        }
    }
}