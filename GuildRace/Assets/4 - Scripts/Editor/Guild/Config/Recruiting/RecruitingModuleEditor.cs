using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="RecruitingModuleData"/>
    /// </summary>
    public class RecruitingModuleEditor
    {
        private PropertyElement minRequestCountField;
        private PropertyElement maxRequestCountField;
        private PropertyElement requestLifetimeField;

        private PropertyElement minNextRequestTimeField;
        private PropertyElement maxNextRequestTimeField;

        private PropertyElement weightSelectedRoleField;
        private PropertyElement weightUnselectedRoleField;

        private PropertyElement characterGroupsWeightsList;
        private PropertyElement minEquipLevelField;
        private EquipGeneratorPhaseBlock firstPhaseBlock;
        private EquipGeneratorPhaseBlock lastPhaseBlock;

        private DefaultCharactersList defaultCharactersList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("recruitingModule");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Common", CreateCommonTab);
            tabs.CreateTab("Equips Generator", CreateEquipsGeneratorTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            var rData = GetData(data);

            minRequestCountField = root.CreateProperty();
            minRequestCountField.BindProperty("minRequestCount", rData);

            maxRequestCountField = root.CreateProperty();
            maxRequestCountField.BindProperty("maxRequestCount", rData);

            requestLifetimeField = root.CreateProperty();
            requestLifetimeField.BindProperty("requestLifetime", rData);

            root.CreateSpace();

            minNextRequestTimeField = root.CreateProperty();
            minNextRequestTimeField.BindProperty("minNextRequestTime", rData);

            maxNextRequestTimeField = root.CreateProperty();
            maxNextRequestTimeField.BindProperty("maxNextRequestTime", rData);

            root.CreateSpace();

            weightSelectedRoleField = root.CreateProperty();
            weightSelectedRoleField.BindProperty("weightSelectedRole", rData);

            weightUnselectedRoleField = root.CreateProperty();
            weightUnselectedRoleField.BindProperty("weightUnselectedRole", rData);

            root.CreateHeader("Default Characters");

            defaultCharactersList = root.CreateElement<DefaultCharactersList>();
            defaultCharactersList.BindProperty("defaultCharacters", rData);
        }

        private void CreateEquipsGeneratorTab(VisualElement root, SerializedData data)
        {
            var rData = GetData(data);

            characterGroupsWeightsList = root.CreateProperty();
            characterGroupsWeightsList.BindProperty("characterGroupsWeights", rData);

            root.CreateSpace();

            minEquipLevelField = root.CreateProperty();
            minEquipLevelField.BindProperty("minEquipLevel", rData);

            firstPhaseBlock = new EquipGeneratorPhaseBlock();
            firstPhaseBlock.Create("firstPhase", root, rData);

            lastPhaseBlock = new EquipGeneratorPhaseBlock();
            lastPhaseBlock.Create("lastPhase", root, rData);
        }
    }
}