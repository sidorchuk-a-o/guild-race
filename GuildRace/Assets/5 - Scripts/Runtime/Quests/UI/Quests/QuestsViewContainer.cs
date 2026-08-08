using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Quests
{
    public class QuestsViewContainer : MonoBehaviour
    {
        [SerializeField] private QuestViewItem[] items;

        private readonly ReactiveEvent<QuestVM> onSelect = new();

        public ReadOnlyReactiveEvent<QuestVM> OnSelect => onSelect;

        public void Init(QuestsVM questsVM, CompositeDisp disp)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var questVM = questsVM[i];

                var hasVM = questVM != null;

                if (hasVM)
                {
                    item.Init(questVM, disp);

                    item.OnSelect
                        .Subscribe(onSelect.OnNext)
                        .AddTo(disp);
                }

                item.SetActive(hasVM);
            }
        }
    }
}