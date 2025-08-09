using System.Collections.Generic;
using AD.Services.Store;

namespace Game.Store
{
    public class EquipsRewardVM : RewardVM
    {
        private readonly EquipsReward data;

        public EquipsRewardVM(EquipsReward data, StoreVMFactory storeVMF) : base(data, storeVMF)
        {
            this.data = data;
        }

        protected override IEnumerable<RewardPreviewVM> GetRewardPreviews()
        {
            yield return new(new(data.PreviewKey, data.Count), null, storeVMF);
        }
    }
}