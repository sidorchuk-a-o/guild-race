using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game
{
    public static class GoogleSheetsImportExt
    {
        public static AssetReference AddressableFileParse(this string fileName)
        {
            var fileRef = new AssetReference();
            var fileAsset = AssetsEditorUtils.LoadAssetByName<Object>(fileName);

            fileRef.SetEditorAsset(fileAsset);

            return fileRef;
        }
    }
}