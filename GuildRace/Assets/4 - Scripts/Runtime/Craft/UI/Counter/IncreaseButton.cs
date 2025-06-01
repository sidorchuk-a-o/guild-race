using AD.ToolsCollection;
using AD.UI;
using System;
using UniRx;
using UnityEngine;

namespace Game.Craft
{
    public class IncreaseButton : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private UIButton button;

        private readonly Subject<int> onClick = new();

        public IObservable<int> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(() => onClick.OnNext(value))
                .AddTo(this);
        }
    }
}