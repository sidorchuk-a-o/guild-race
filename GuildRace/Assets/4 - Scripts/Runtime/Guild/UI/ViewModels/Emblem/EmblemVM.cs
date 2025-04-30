using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Guild
{
    public class EmblemVM : ViewModel
    {
        private readonly EmblemInfo info;
        private readonly EmblemParams emblemParams;

        private Sprite symbolSprite;
        private Material backgroundSprite;

        private AddressableSprite symbolAsset;
        private AddressableMaterial backgroundAsset;

        public Color SymbolColor { get; }
        public Color[] BackgroundColors { get; }

        public EmblemVM(EmblemInfo info, EmblemParams emblemParams)
        {
            this.info = info;
            this.emblemParams = emblemParams;

            SymbolColor = emblemParams.GetColor(info.SymbolColor);

            BackgroundColors = info.BackgroundColors
                .Select(x => emblemParams.GetColor(x))
                .ToArray();
        }

        protected override void InitSubscribes()
        {
        }

        public async UniTask<Sprite> GetSymbol()
        {
            if (symbolSprite == null)
            {
                symbolAsset = emblemParams.GetSymbol(info.Symbol);
                symbolAsset.AddTo(this);

                symbolSprite = await symbolAsset.LoadAsync();
            }

            return symbolSprite;
        }

        public async UniTask<Material> GetBackground()
        {
            if (backgroundSprite == null)
            {
                backgroundAsset = emblemParams.GetDivision(info.Background);
                backgroundAsset.AddTo(this);

                backgroundSprite = await backgroundAsset.LoadAsync();
            }

            return backgroundSprite;
        }
    }
}