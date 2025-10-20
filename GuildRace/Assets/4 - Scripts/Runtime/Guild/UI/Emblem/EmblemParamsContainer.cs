using System.Linq;
using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class EmblemParamsContainer : UIContainer
    {
        [Header("Preview")]
        [SerializeField] private EmblemPreviewContainer emblemPreview;

        [Header("Params")]
        [SerializeField] private IndexSwitch symbolsSwitch;
        [SerializeField] private IndexSwitch symbolColorSwitch;
        [SerializeField] private IndexSwitch backgroundsSwitch;
        [SerializeField] private IndexSwitch[] backgroundColorSwitches;

        private GuildConfig guildConfig;

        public EmblemEM EmblemEM => new()
        {
            Symbol = symbolsSwitch.Index.Value,
            Background = backgroundsSwitch.Index.Value,
            SymbolColor = symbolColorSwitch.Index.Value,
            BackgroundColors = backgroundColorSwitches
                .Where(x => x.gameObject.activeSelf)
                .Select(x => x.Index.Value)
                .ToList()
        };

        [Inject]
        public void Inject(GuildConfig guildConfig)
        {
            this.guildConfig = guildConfig;
        }

        private void Awake()
        {
            symbolsSwitch.Index
                .SilentSubscribe(async x => await emblemPreview.SetSymbol(x))
                .AddTo(this);

            backgroundsSwitch.Index
                .SilentSubscribe(async x =>
                {
                    await emblemPreview.SetBackground(x);

                    UpdateColorSwitches();
                })
                .AddTo(this);

            symbolColorSwitch.Index
                .SilentSubscribe(x => emblemPreview.SetSymbolColor(x))
                .AddTo(this);

            for (var i = 0; i < backgroundColorSwitches.Length; i++)
            {
                var switchIndex = i;
                var colorSwitch = backgroundColorSwitches[i];

                colorSwitch.Index
                    .SilentSubscribe(x => emblemPreview.SetBackgroundColor(switchIndex, x))
                    .AddTo(this);
            }
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var emblemEM = guildConfig.DefaultEmblemEM;

            InitSwitches(emblemEM);

            await emblemPreview.Init(emblemEM);

            UpdateColorSwitches();
        }

        private void InitSwitches(EmblemEM emblemEM)
        {
            var emblemParams = guildConfig.EmblemParams;
            var symbolsCount = emblemParams.Symbols.Count;
            var backgroundsCount = emblemParams.Divisions.Count;
            var colorsCount = emblemParams.Colors.Count;

            symbolsSwitch.Init(symbolsCount, emblemEM.Symbol);
            symbolColorSwitch.Init(colorsCount, emblemEM.SymbolColor);
            backgroundsSwitch.Init(backgroundsCount, emblemEM.Background);

            for (var i = 0; i < backgroundColorSwitches.Length; i++)
            {
                var value = emblemEM.BackgroundColors.ElementAtOrDefault(i);

                backgroundColorSwitches[i].Init(colorsCount, value);
            }
        }

        private void UpdateColorSwitches()
        {
            var colorCount = emblemPreview.BackgroundColorsCount;
            var switchesCount = backgroundColorSwitches.Length;

            for (var i = 0; i < switchesCount; i++)
            {
                var colorSwitch = backgroundColorSwitches[i];

                colorSwitch.SetActive(i < colorCount);

                emblemPreview.SetBackgroundColor(i, colorSwitch.Index.Value);
            }
        }
    }
}