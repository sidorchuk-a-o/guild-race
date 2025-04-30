using System.Threading;
using AD.Services.Pools;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using VContainer;

namespace Game.Guild
{
    public class EmblemPreviewContainer : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image symbolImage;

        private EmblemParams emblemParams;
        private PoolContainer<Sprite> spritePool;
        private PoolContainer<Material> materialPool;

        private CancellationTokenSource spriteToken;
        private CancellationTokenSource materialToken;

        public int BackgroundColorsCount { get; private set; }

        [Inject]
        public void Inject(GuildConfig guildConfig, IPoolsService poolsService)
        {
            emblemParams = guildConfig.EmblemParams;

            spritePool = poolsService.CreateAssetPool<Sprite>();
            materialPool = poolsService.CreateAssetPool<Material>();

            spritePool.AddTo(this);
            materialPool.AddTo(this);
        }

        public async UniTask Init(EmblemEM emblemEM)
        {
            await SetSymbol(emblemEM.Symbol);
            await SetBackground(emblemEM.Background);

            SetSymbolColor(emblemEM.SymbolColor);

            for (var i = 0; i < emblemEM.BackgroundColors.Count; i++)
            {
                SetBackgroundColor(i, emblemEM.BackgroundColors[i]);
            }
        }

        public async UniTask SetSymbol(int symbolIndex)
        {
            var token = new CancellationTokenSource();

            spriteToken?.Cancel();
            spriteToken = token;

            var spriteAsset = emblemParams.GetSymbol(symbolIndex);
            var sprite = await spritePool.RentAsync(spriteAsset);

            if (token.IsCancellationRequested)
            {
                return;
            }

            symbolImage.sprite = sprite;
        }

        public void SetSymbolColor(int colorIndex)
        {
            var color = emblemParams.GetColor(colorIndex);

            symbolImage.color = color;
        }

        public async UniTask SetBackground(int backgroundIndex)
        {
            var token = new CancellationTokenSource();

            materialToken?.Cancel();
            materialToken = token;

            var materialAsset = emblemParams.GetDivision(backgroundIndex);
            var material = await materialPool.RentAsync(materialAsset);

            if (token.IsCancellationRequested)
            {
                return;
            }

            backgroundImage.material = new(material);

            BackgroundColorsCount = GetBackgroundColorCount(material.shader);
        }

        public void SetBackgroundColor(int index, int colorIndex)
        {
            if (index >= BackgroundColorsCount)
            {
                return;
            }

            var colorParam = $"_Color{index + 1}";
            var color = emblemParams.GetColor(colorIndex);

            backgroundImage.material.SetColor(colorParam, color);
        }

        // == Utils ==

        private int GetBackgroundColorCount(Shader shader)
        {
            var propCount = shader.GetPropertyCount();
            var colorsCount = 0;

            for (var i = 0; i < propCount; i++)
            {
                var propName = shader.GetPropertyName(i);
                var propType = shader.GetPropertyType(i);

                if (propType == ShaderPropertyType.Color && propName.StartsWith("_Color"))
                {
                    colorsCount++;
                }
            }

            return colorsCount;
        }
    }
}