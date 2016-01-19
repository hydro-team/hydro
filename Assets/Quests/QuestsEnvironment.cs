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

        public void BeginQuest(Quest quest) {
            gameObject.AddComponent<ActiveQuest>().Begin(quest);
        }

        public T BeginQuest<T>(Quest quest) {
            gameObject.AddComponent<ActiveQuest>().Begin(quest);
            return GetComponent<T>();
        }

        public void NewObjective(ActiveQuest quest, QuestObjective objective) {
            Trigger(OnNewObjective, quest, objective);
        }

        public void ObjectiveSucceeded(ActiveQuest quest, QuestObjective objective) {
            Trigger(OnObjectiveSucceeded, quest, objective);
        }

        public void ObjectiveFailed(ActiveQuest quest, QuestObjective objective) {
            Trigger(OnObjectiveFailed, quest, objective);
        }

        public void QuestStarted(ActiveQuest quest) {
            Trigger(OnQuestStarted, quest);
        }

        public void QuestSucceeded(ActiveQuest quest) {
            Trigger(OnQuestSucceeded, quest);
        }

        public void QuestFailed(ActiveQuest quest) {
            Trigger(OnQuestFailed, quest);
        }

        void Trigger(Action<ActiveQuest, QuestObjective> action, ActiveQuest quest, QuestObjective objective) {
            if (action != null) { action.Invoke(quest, objective); }
        }

        void Trigger(Action<ActiveQuest> action, ActiveQuest quest) {
            if (action != null) { action.Invoke(quest); }
        }
    }
}
