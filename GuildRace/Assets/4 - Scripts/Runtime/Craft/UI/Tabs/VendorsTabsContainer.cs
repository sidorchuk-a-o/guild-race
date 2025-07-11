﻿using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Craft
{
    public class VendorsTabsContainer : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private VendorTab vendorTabPrefab;

        [Header("Selected Tab")]
        [SerializeField] private RecipesScrollView recipesScroll;

        private readonly List<VendorTab> tabs = new();

        private readonly ReactiveProperty<VendorVM> vendorVM = new();
        private readonly ReactiveProperty<RecipeVM> recipeVM = new();

        private IObjectResolver resolver;
        private VendorsVM vendorsVM;

        public IReadOnlyReactiveProperty<VendorVM> VendorVM => vendorVM;
        public IReadOnlyReactiveProperty<RecipeVM> RecipeVM => recipeVM;

        [Inject]
        public void Inject(CraftVMFactory craftVMF, IObjectResolver resolver)
        {
            this.resolver = resolver;

            vendorsVM = craftVMF.GetVendors();
        }

        private void Awake()
        {
            foreach (var tab in tabs)
            {
                tab.OnClick
                    .Subscribe(SelectTab)
                    .AddTo(this);
            }
        }

        public void Init(CompositeDisp disp)
        {
            vendorsVM.AddTo(disp);

            InitVendorTabs(disp);

            SelectTab(vendorVM.Value);

            recipesScroll.OnSelect
                .Subscribe(SelectRecipeCallback)
                .AddTo(disp);
        }

        private void InitVendorTabs(CompositeDisp disp)
        {
            for (var i = 0; i < vendorsVM.Count; i++)
            {
                var vendorVM = vendorsVM[i];
                var vendorTab = tabs.ElementAtOrDefault(i);

                if (vendorTab == null)
                {
                    vendorTab = Instantiate(vendorTabPrefab, transform);

                    resolver.InjectGameObject(vendorTab.gameObject);

                    tabs.Add(vendorTab);
                }

                vendorTab.Init(vendorVM, disp);
            }
        }

        private void SelectTab(VendorVM vendorVM)
        {
            this.vendorVM.Value?.SetSelectState(false);
            this.vendorVM.Value = vendorVM;

            UpdateCurrentTabView();
        }

        private void UpdateCurrentTabView()
        {
            vendorVM.Value ??= vendorsVM.FirstOrDefault();
            vendorVM.Value.SetSelectState(true);

            recipesScroll.Init(vendorVM.Value.RecipesVM, forcedReset: true);

            SetRecipe(recipeVM.Value);
        }

        private void SelectRecipeCallback(RecipeVM recipeVM)
        {
            SetRecipe(recipeVM);
        }

        private void SetRecipe(RecipeVM value)
        {
            if (recipeVM.Value == value)
            {
                return;
            }

            recipeVM.Value?.SetSelectState(false);
            recipeVM.Value = value;
            recipeVM.Value?.SetSelectState(true);
        }
    }
}