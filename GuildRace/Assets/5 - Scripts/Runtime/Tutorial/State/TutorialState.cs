using System.Collections.Generic;
using AD.Management;
using AD.Save;

namespace AD.Game.Tutorial
{
    public class TutorialState : ServiceState<TutorialSM>
    {
        private readonly List<string> showedComponents = new();

        public override string SaveKey => "tutorial";
        public override SaveSource SaveSource => SaveSource.App;

        public IReadOnlyList<string> ShowedComponents => showedComponents;

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
            return new()
            {
                ShowedComponents = showedComponents
            };
        }

        protected override void CreateDefaultState()
        {
        }

        protected override void ReadSave(TutorialSM save)
        {
            showedComponents.AddRange(save.ShowedComponents);
        }
    }
}