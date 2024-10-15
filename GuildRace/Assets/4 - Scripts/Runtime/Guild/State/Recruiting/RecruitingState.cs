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
        private readonly List<ClassWeightInfo> classWeights = new();

        public IReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public DateTime NextRequestTime { get; private set; }
        public IJoinRequestsCollection Requests => requests;
        public IReadOnlyList<ClassWeightInfo> ClassWeights => classWeights;

        public RecruitingState(GuildConfig config, GuildState state)
        {
            this.state = state;
            this.config = config;
        }

        public void SetNextRequestTime(DateTime value)
        {
            NextRequestTime = value;
        }

        public void CreateRequest()
        {
            var id = GuidUtils.Generate();
            var nickname = GetNickname(id);

            var classId = classWeights.WeightedSelection(GetClassWeight).ClassId;
            var classData = config.CharactersModule.GetClass(classId);
            var specData = classData.Specs.RandomValue();

            var recruitRank = state.GuildRanks.Last();

            var character = new CharacterInfo(id, nickname, classData.Id);

            character.SetGuildRank(recruitRank.Id);
            character.SetSpecialization(specData.Id);

            var createdTime = DateTime.Now;
            var request = new JoinRequestInfo(character, createdTime);

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

        private float GetClassWeight(ClassWeightInfo classWeight)
        {
            return classWeight.IsEnabled.Value
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

        public void SetClassWeightState(ClassId classId, bool isEnabled)
        {
            var weight = classWeights.FirstOrDefault(x => x.ClassId == classId);

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
                ClassWeights = classWeights
            };
        }

        public void ReadSave(RecruitingSM save)
        {
            if (save == null)
            {
                isEnabled.Value = true;
                requests.AddRange(CreateDefaultRequests());
                classWeights.AddRange(CreateDefaultClassWeights());

                return;
            }

            isEnabled.Value = save.IsEnabled;
            NextRequestTime = save.NextRequestTime;

            requests.AddRange(save.Requests);
            classWeights.AddRange(save.ClassWeights);
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

        private IEnumerable<ClassWeightInfo> CreateDefaultClassWeights()
        {
            return config.CharactersModule.Classes.Select(x =>
            {
                return new ClassWeightInfo(x.Id, isEnabled: true);
            });
        }

        private static string GetNickname(string id)
        {
            return $"Игрок ({id[..7]})";
        }
    }
}