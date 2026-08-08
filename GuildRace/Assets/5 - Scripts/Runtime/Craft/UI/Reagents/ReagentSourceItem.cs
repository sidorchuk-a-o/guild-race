using AD.Game.Instances;
using AD.ToolsCollection;
using AD.UI;
using UnityEngine;

namespace AD.Game.Craft
{
    public class ReagentSourceItem : MonoBehaviour
    {
        [SerializeField] private UIText labelText;

        public void Init(InstanceRewardVM itemVM, CompositeDisp disp)
        {
            var bossKey = itemVM.UnitVM.NameKey;
            var instanceKey = itemVM.InstanceVM.NameKey;

            labelText.SetTextParams(new(labelText.LocalizeKey, bossKey, instanceKey));
        }
    }
}