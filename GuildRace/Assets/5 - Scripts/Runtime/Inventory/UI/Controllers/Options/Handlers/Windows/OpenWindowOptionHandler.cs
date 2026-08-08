using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
{
    public abstract class OpenWindowOptionHandler : OptionHandler
    {
        [SerializeField] private AssetReference windowRef;

        public override UniTask StartProcess(OptionContext context)
        {
            InventoryWindowsController.OpenWindow(new()
            {
                ItemId = context.SelectedItemId,
                WindowRef = windowRef
            });

            return UniTask.CompletedTask;
        }
    }
}