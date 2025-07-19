using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;

namespace Game.Tutorial
{
    public class TutorialContainer : UIContainer
    {
        public void SetId(string containerId)
        {
        }

        protected override UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            return base.Init(parameters, disp, ct);
        }
    }
}