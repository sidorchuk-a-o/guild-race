﻿using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.ToolsCollection;
using Game.Inventory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Craft
{
    public class RecycleSlotContainer : ItemSlotContainer
    {
        [Header("Currency Preview")]
        [SerializeField] private GameObject currencyContainer;
        [SerializeField] private Image currencyIconImage;
        [SerializeField] private UIText currencyAmountText;

        [Header("Reagents Preview")]
        [SerializeField] private GameObject reagentsContainer;
        [SerializeField] private RecycleReagentItem reagentItemPrefab;

        private readonly CompositeDisp disp = new();
        private CancellationTokenSource previewToken;

        private readonly List<RecycleReagentItem> reagentItems = new();

        private CraftVMFactory craftVMF;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        public override async void ShowPickupPreview(ItemVM itemVM, PickupResult pickupResult)
        {
            var token = new CancellationTokenSource();
            var recyclingVM = craftVMF.GetRecyclingParams(itemVM.Id);

            disp.Clear();
            disp.AddTo(this);

            previewToken?.Cancel();
            previewToken = token;

            recyclingVM.AddTo(disp);

            if (recyclingVM is RecyclingReagentResultVM recyclingReagentVM)
            {
                var currencyVM = recyclingReagentVM.CurrencyVM;

                currencyAmountText.SetTextParams(new(currencyVM.AmountStr.Value));
                currencyIconImage.sprite = await currencyVM.LoadIcon();

                currencyContainer.SetActive(true);
            }

            if (recyclingVM is RecyclingItemResultVM recyclingItemVM)
            {
                var reagentsVM = recyclingItemVM.ReagentsVM;
                var count = Mathf.Max(reagentsVM.Count, reagentItems.Count);

                for (var i = 0; i < count; i++)
                {
                    var reagentVM = recyclingItemVM.ReagentsVM.ElementAtOrDefault(i);
                    var reagentItem = reagentItems.ElementAtOrDefault(i);

                    if (reagentItem == null)
                    {
                        reagentItem = Instantiate(reagentItemPrefab, reagentsContainer.transform);

                        reagentItems.Add(reagentItem);
                    }

                    if (reagentVM != null)
                    {
                        reagentItem.SetActive(true);

                        await reagentItem.Init(reagentVM, token);

                        if (token.IsCancellationRequested) return;
                    }
                    else
                    {
                        reagentItem.SetActive(false);
                    }
                }

                reagentsContainer.SetActive(true);
            }

            base.ShowPickupPreview(itemVM, pickupResult);
        }

        public override void ResetPickupPreview()
        {
            base.ResetPickupPreview();

            currencyContainer.SetActive(false);
            reagentsContainer.SetActive(false);

            disp.Clear();
            previewToken?.Cancel();
        }
    }
}