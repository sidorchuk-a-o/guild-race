using AD.ToolsCollection;
using System;
using UniRx;
using UnityEngine;

namespace Game.Quests
{
    public class QuestsViewContainer : MonoBehaviour
    {
        [SerializeField] private QuestViewItem[] items;

        private readonly Subject<QuestVM> onSelect = new();

        public IObservable<QuestVM> OnSelect => onSelect;

        public void Init(QuestsVM questsVM, CompositeDisp disp)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var questVM = questsVM[i];

                if (questVM != null)
                {
                    item.Init(questVM, disp);

                    item.OnSelect
                        .Subscribe(onSelect.OnNext)
                        .AddTo(disp);
                }
            }
        }
    }
}