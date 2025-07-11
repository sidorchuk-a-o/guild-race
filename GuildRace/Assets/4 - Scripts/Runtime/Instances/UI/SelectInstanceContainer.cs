﻿using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Game.Instances
{
    public class SelectInstanceContainer : UIContainer
    {
        [Header("Header")]
        [SerializeField] private UIText headerText;
        [SerializeField] private LocalizeKey headerKey;

        [Header("Season")]
        [SerializeField] private UIText seasonDescText;

        [Header("Raid")]
        [SerializeField] private InstanceItem raidItem;

        [Header("Dungeons")]
        [SerializeField] private List<InstanceItem> dungeonItems;

        private SeasonVM seasonVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            seasonVM = instancesVMF.GetFirstSeason();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            seasonVM.AddTo(disp);

            headerText.SetTextParams(new(headerKey, seasonVM.NameKey));
            seasonDescText.SetTextParams(seasonVM.DescKey);

            await InitInstances(ct);
        }

        private async UniTask InitInstances(CancellationTokenSource ct)
        {
            var tasks = ListPool<UniTask>.Get();

            tasks.Add(raidItem.Init(seasonVM.RaidVM, ct));
            tasks.AddRange(dungeonItems.Select((x, i) => x.Init(seasonVM.DungeonsVM[i], ct)));

            await UniTask.WhenAll(tasks);

            tasks.ReleaseListPool();
        }
    }
}