using AD.States;
using AD.Services.Save;
using System.Collections.Generic;
using VContainer;

namespace Game.Tutorial
{
    public class TutorialState : State<TutorialSM>
    {
        private readonly List<string> showedComponents = new();

        public override string SaveKey => TutorialSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IReadOnlyList<string> ShowedComponents => showedComponents;

        public TutorialState(IObjectResolver resolver) : base(resolver)
        {
        }

        public void AddComponent(string componentId)
        {
            showedComponents.Add(componentId);

            MarkAsDirty(true);
        }

        public void ResetProgress()
        {
            showedComponents.Clear();

            MarkAsDirty(true);
        }

        // == Save ==

        protected override TutorialSM CreateSave()
        {
            return new TutorialSM
            {
                ShowedComponents = showedComponents
            };
        }

        protected override void ReadSave(TutorialSM save)
        {
            if (save == null)
            {
                return;
            }

            showedComponents.AddRange(save.ShowedComponents);
        }
    }
}