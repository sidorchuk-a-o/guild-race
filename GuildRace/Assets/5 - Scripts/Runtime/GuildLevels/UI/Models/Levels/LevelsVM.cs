using System.Collections.Generic;
using AD.Router;

namespace AD.Game.GuildLevels
{
    public class LevelsVM : VMCollection<LevelInfo, LevelVM>
    {
        private readonly GuildLevelsVMFactory levelsVMF;

        public int LastReadyToCompleteIndex
        {
            get
            {
                var index = FindLastIndex(x => x.ReadyToUnlock.CurrentValue);

                return index != -1 ? index : Count - 1;
            }
        }

        public LevelsVM(IReadOnlyCollection<LevelInfo> values, GuildLevelsVMFactory levelsVMF) : base(values)
        {
            this.levelsVMF = levelsVMF;
        }

        protected override LevelVM Create(LevelInfo value)
        {
            return new(value, levelsVMF);
        }
    }
}