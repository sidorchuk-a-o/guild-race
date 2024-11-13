using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    /// <summary>
    /// Import: <see cref="ItemData"/>
    /// </summary>
    public abstract class ItemDataImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "Id";
        public override string TitleKey => "Id";

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            var localizeKey = row[IdKey].LocalizeKeyParse();
            var slot = new ItemSlot(row["eq_slot_id"].IntParse());
            var source = new ItemSource(row["source_id"].IntParse());

            var iconName = row["icon_name"];
            var iconRef = new AssetReference();
            var iconAsset = AssetsEditorUtils.LoadAssetByName<Object>(iconName) 
                         ?? AssetsEditorUtils.LoadAssetByName<Object>("cloth-chest-10015"); // TEMP

            iconRef.SetEditorAsset(iconAsset);

            data.GetProperty("iconRef").SetValue(iconRef);
            data.GetProperty("nameKey").SetValue(localizeKey);
            data.GetProperty("slot").SetValue(slot);
            data.GetProperty("source").SetValue(source);
        }
    }
}