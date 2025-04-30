using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class EmblemContainer : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image symbolImage;

        private EmblemVM emblemVM;

        public async UniTask Init(EmblemVM emblemVM)
        {
            this.emblemVM = emblemVM;

            await LoadSymbol();
            await LoadBackground();
        }

        private async UniTask LoadSymbol()
        {
            var color = emblemVM.SymbolColor;
            var symbol = await emblemVM.GetSymbol();

            symbolImage.color = color;
            symbolImage.sprite = symbol;
        }

        private async UniTask LoadBackground()
        {
            var colors = emblemVM.BackgroundColors;
            var background = await emblemVM.GetBackground();

            backgroundImage.material = new(background);

            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                var colorParam = $"_Color{i + 1}";

                backgroundImage.material.SetColor(colorParam, color);
            }
        }
    }
}