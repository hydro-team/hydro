using UnityEngine;
using Quests;
using System;
using System.Collections.Generic;

public class FrogQuest : Quest {

    public override string Name() {
        return "Jump Over";
    }

    public override string Description() {
        return "Help the tadpole to become a frog.";
    }

    public override IList<QuestObjective> FirstObjectives() {
        return new List<QuestObjective> { new CollectSeaweed() };
    }

    public override void Initialize(QuestsEnvironment environment) {
        environment.gameObject.AddComponent<Context>();
    }

    public class Context : MonoBehaviour {
        public bool collectedSeaweed = false;
        public bool feedTadpole = false;
        public bool jumpedOver = false;
    }

    class CollectSeaweed : QuestObjective {

        public override string Description() {
            return "Find and collect the seaweed";
        }

        public override bool IsOptional() {
            return false;
        }

        public override IList<QuestObjective> NextObjectives() {
            return new List<QuestObjective> { new FeedTadpole() };
        }

        public override ProgressStatus StatusIn(QuestsEnvironment environment) {
            return environment.GetComponent<Context>().collectedSeaweed ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
        }
    }

    class FeedTadpole : QuestObjective {

        public override string Description() {
            return "Feed the tadpole with the seaweed";
        }

        public override bool IsOptional() {
            return false;
        }

        public override IList<QuestObjective> NextObjectives() {
            return new List<QuestObjective> { new JumpOver() };
        }

        public override ProgressStatus StatusIn(QuestsEnvironment environment) {
            return environment.GetComponent<Context>().feedTadpole ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
        }
    }

    class JumpOver : QuestObjective {

        public override string Description() {
            return "Ask the grown frog for help";
        }

        public override bool IsOptional() {
            return false;
        }

        public override IList<QuestObjective> NextObjectives() {
            return new List<QuestObjective> { };
        }

        public override ProgressStatus StatusIn(QuestsEnvironment environment) {
            return environment.GetComponent<Context>().jumpedOver ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
        }
    }
}
