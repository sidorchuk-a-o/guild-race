using System.Collections.Generic;
using System.Linq;

namespace Game.Tutorial
{
    public class TutorialSM
    {
        public const string key = "tutorial";

        [ES3Serializable] private List<string> showedComponents;

        public IEnumerable<string> ShowedComponents
        {
            get => showedComponents;
            set => showedComponents = value.ToList();
        }
    }
}