using RewardResult = AD.Services.Store.RewardResult;

namespace Game.Store
{
    public class EquipRewardResult : RewardResult
    {
        public string ItemId { get; set; }
        public int ItemDataId { get; set; }
    }
}