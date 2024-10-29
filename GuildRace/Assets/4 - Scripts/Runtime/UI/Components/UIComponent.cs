using AD.ToolsCollection;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UniRx;

namespace Game.UI
{
    public abstract class UIComponent<T> : MonoBehaviour
        where T : UIComponent<T>
    {
        public static event Action<T> GlobalRegistered;
        public static event Action<T> GlobalUnregistered;
        public static event Action<T> GlobalEnabled;
        public static event Action<T> GlobalDisabled;

        private static readonly List<T> components = new();

        public static IReadOnlyList<T> AllComponents => components;
        public static IEnumerable<T> EnabledComponents => components.Where(x =>
        {
            return x.enabled && x.gameObject.activeInHierarchy;
        });

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
        }

        protected virtual void OnDisable()
        {
            GlobalDisabled.SafeInvoke(this as T);
        }
    }
}