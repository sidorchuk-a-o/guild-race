using AD.UI;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine;

namespace Game.Craft
{
    public class ReagentSourceItem : MonoBehaviour
    {
        [SerializeField] private UIText labelText;

        public void Init(InstanceRewardVM itemVM, CompositeDisp disp)
        {
            var bossKey = itemVM.UnitVM.NameKey;
            var instenceKey = itemVM.InstanceVM.NameKey;

            labelText.SetTextParams(new(labelText.LocalizeKey, bossKey, instenceKey));
        }
    }
}