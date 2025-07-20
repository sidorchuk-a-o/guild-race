using AD.UI;
using AD.Services.Router;
using AD.Services.Localization;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Tutorial
{
    public class TutorialContainer : UIContainer
    {
        [SerializeField] private UIText titleText;
        [SerializeField] private UIText descText;

        [Header("Text")]
        [SerializeField] private LocalizeKey titleKey;
        [SerializeField] private LocalizeKey descKey;

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            titleText.SetTextParams(titleKey);
            descText.SetTextParams(descKey);
        }
    }
}