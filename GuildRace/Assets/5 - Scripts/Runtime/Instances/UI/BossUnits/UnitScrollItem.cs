using System.Threading;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace AD.Game.Instances
{
    public class UnitScrollItem : VMScrollItem<UnitVM>
    {
        [Header("Unit")]
        [SerializeField] private UIText nameText;
        [SerializeField] private GameObject hourglassContainer;

        protected override UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            nameText.SetTextParams(ViewModel.NameKey);

            ViewModel.ActiveInstanceVM
                .Subscribe(_ => hourglassContainer.SetActive(ViewModel.HasActiveInstance))
                .AddTo(disp);

            return UniTask.CompletedTask;
        }
    }
}