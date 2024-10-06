using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UniRx;
using AD.ToolsCollection;

namespace Game.Components
{
    public abstract class GameComponent<T> : MonoBehaviour
        where T : GameComponent<T>
    {
        public static event Action<T> GlobalRegistered;
        public static event Action<T> GlobalUnregistered;
        public static event Action<T> GlobalEnabled;
        public static event Action<T> GlobalDisabled;

        private static readonly List<T> components = new();

        private readonly CompositeDisp disp = new();

        public static IReadOnlyList<T> AllComponents => components;
        public static IEnumerable<T> EnabledComponents => components.Where(x =>
        {
            return x.enabled && x.gameObject.activeInHierarchy;
        });

        public static T GetComponent()
        {
            return EnabledComponents.FirstOrDefault();
        }

        protected virtual void Awake()
        {
            components.Add(this as T);

            GlobalRegistered.SafeInvoke(this as T);
        }

        protected virtual void OnDestroy()
        {
            components.Remove(this as T);

            GlobalUnregistered.SafeInvoke(this as T);
        }

        protected virtual void OnEnable()
        {
            GlobalEnabled.SafeInvoke(this as T);

            disp.Clear();
            disp.AddTo(this);

            InitSubscribes(disp);
        }

        protected virtual void OnDisable()
        {
            GlobalDisabled.SafeInvoke(this as T);

            disp.Clear();
        }

        protected virtual void InitSubscribes(CompositeDisp disp)
        {
        }
    }
}