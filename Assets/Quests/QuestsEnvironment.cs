using UnityEngine;
using System;

namespace Quests {

    /// <summary>
    /// Defines events to inform subscribers when quests and objectives are added, or when they succeed or fail.
    /// It is also used by active quest as a container for context components.
    /// </summary>
    public class QuestsEnvironment : MonoBehaviour {

        public event Action<ActiveQuest, QuestObjective> OnNewObjective, OnObjectiveSucceeded, OnObjectiveFailed;
        public event Action<ActiveQuest> OnQuestStarted, OnQuestSucceeded, OnQuestFailed;

        public void NewObjective(ActiveQuest quest, QuestObjective objective) {
            OnNewObjective(quest, objective);
        }

        public void ObjectiveSucceeded(ActiveQuest quest, QuestObjective objective) {
            OnObjectiveSucceeded(quest, objective);
        }

        public void ObjectiveFailed(ActiveQuest quest, QuestObjective objective) {
            OnObjectiveFailed(quest, objective);
        }

        public void QuestStarted(ActiveQuest quest) {
            OnQuestStarted(quest);
        }

        public void QuestSucceeded(ActiveQuest quest) {
            OnQuestSucceeded(quest);
        }

        public void QuestFailed(ActiveQuest quest) {
            OnQuestFailed(quest);
        }
    }
}
