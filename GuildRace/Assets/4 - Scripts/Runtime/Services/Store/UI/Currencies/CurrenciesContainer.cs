using System.Collections.Generic;
using System.Threading;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Store
{
    public class CurrenciesContainer : MonoBehaviour
    {
        [SerializeField] private List<CurrencyItem> currencyItems;

        public void Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            currencyItems.ForEach(x => x.Init(disp, ct));
        }
    }
}