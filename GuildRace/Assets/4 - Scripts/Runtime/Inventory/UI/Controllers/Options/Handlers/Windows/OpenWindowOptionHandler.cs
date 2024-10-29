#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class OpenWindowOptionHandler : OptionHandler
    {
        [SerializeField] private AssetReference windowRef;

        public override async UniTask StartProcess(OptionContext context)
        {
            InventoryWindowsController.OpenWindow(new OpenItemWindowArgs
            {
                ItemId = context.SelectedItemId,
                WindowRef = windowRef
            });
        }
    }
}