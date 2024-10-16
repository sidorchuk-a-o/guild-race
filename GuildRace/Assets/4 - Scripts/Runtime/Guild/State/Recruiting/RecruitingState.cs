using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Guild
{
    public class RecruitingState
    {
        private readonly GuildState state;
        private readonly GuildConfig config;

        private readonly ReactiveProperty<bool> isEnabled = new();
        private readonly JoinRequestsCollection requests = new(null);
        private readonly List<ClassRoleSelectorInfo> classRoleSelectors = new();

        public IReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public DateTime NextRequestTime { get; private set; }
        public IJoinRequestsCollection Requests => requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => classRoleSelectors;

        public RecruitingState(GuildConfig config, GuildState state)
        {
            this.state = state;
            this.config = config;
        }

        public void SetNextRequestTime(DateTime value)
        {
            NextRequestTime = value;
        }

        public void CreateRequest(DateTime? createdTime = null)
        {
            var id = GuidUtils.Generate();
            var nickname = GetNickname(id);

            var roleId = classRoleSelectors
                .WeightedSelection(GetClassRoleWeight)
                .RoleId;

            var (classData, specData) = config.CharactersModule
                .GetSpecializations(roleId)
                .RandomValue();

            var recruitRank = state.GuildRanks.Last();

            var character = new CharacterInfo(id, nickname, classData.Id);

            character.SetGuildRank(recruitRank.Id);
            character.SetSpecialization(specData.Id);

            createdTime ??= DateTime.Now;

            var request = new JoinRequestInfo(character, createdTime.Value);

            requests.Add(request);

            state.MarkAsDirty();
        }

        public int RemoveRequest(string requestId)
        {
            var index = requests.FindIndex(x => x.Id == requestId);

            RemoveRequest(index);

            return index;
        }

        public void RemoveRequest(int requestIndex)
        {
            requests.RemoveAt(requestIndex);

            state.MarkAsDirty();
        }

        private float GetClassRoleWeight(ClassRoleSelectorInfo classRoleSelector)
        {
            return classRoleSelector.IsEnabled.Value
                ? config.RecruitingModule.WeightSelectedRole
                : config.RecruitingModule.WeightUnselectedRole;
        }

        public void SwitchRecruitingState()
        {
            isEnabled.Value = !isEnabled.Value;

            if (isEnabled.Value == false)
            {
                requests.Clear();
            }

            state.MarkAsDirty();
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            var weight = classRoleSelectors.FirstOrDefault(x => x.RoleId == roleId);

            weight.SetActiveState(isEnabled);

            state.MarkAsDirty();
        }

        // == Save ==

        public RecruitingSM CreateSave()
        {
            return new RecruitingSM
            {
                IsEnabled = IsEnabled.Value,
                Requests = Requests,
                NextRequestTime = NextRequestTime,
                ClassRoleSelectors = classRoleSelectors
            };
        }

        public void ReadSave(RecruitingSM save)
        {
            if (save == null)
            {
                isEnabled.Value = true;
                requests.AddRange(CreateDefaultRequests());
                classRoleSelectors.AddRange(CreateDefaultClassRoleSelectors());

                return;
            }

            isEnabled.Value = save.IsEnabled;
            NextRequestTime = save.NextRequestTime;

            requests.AddRange(save.Requests);
            classRoleSelectors.AddRange(save.ClassRoleSelectors);
        }

        private IEnumerable<JoinRequestInfo> CreateDefaultRequests()
        {
            var recruitRank = state.GuildRanks.Last();

            return config.RecruitingModule.DefaultCharacters.Select(x =>
            {
                var id = GuidUtils.Generate();
                var nickname = GetNickname(id);

                var character = new CharacterInfo(id, nickname, x.ClassId);

                character.SetGuildRank(recruitRank.Id);
                character.SetSpecialization(x.SpecId);

                return new JoinRequestInfo(character, createdTime: DateTime.MinValue);
            });
        }

        private IEnumerable<ClassRoleSelectorInfo> CreateDefaultClassRoleSelectors()
        {
            return config.CharactersModule.Roles.Select(x =>
            {
                return new ClassRoleSelectorInfo(x.Id, isEnabled: true);
            });
        }

        private static string GetNickname(string id)
        {
            return $"Игрок ({id[..7]})";
        }
    }
}