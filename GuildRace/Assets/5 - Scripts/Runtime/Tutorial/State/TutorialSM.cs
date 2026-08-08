using System.Collections.Generic;
using System.Linq;

namespace AD.Game.Tutorial
{
    public class TutorialSM
    {
        [ES3Serializable] private List<string> showedComponents;

        public IEnumerable<string> ShowedComponents
        {
            get => showedComponents;
            set => showedComponents = value.ToList();
        }
    }
}