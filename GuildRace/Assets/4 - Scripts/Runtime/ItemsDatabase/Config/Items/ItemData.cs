using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Items
{
    public abstract class ItemData : ScriptableEntity
    {
        [SerializeField] private AssetReference iconRef;

        public AssetReference IconRef => iconRef;
    }
}