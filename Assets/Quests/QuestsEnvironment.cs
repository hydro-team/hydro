using UnityEngine;
using System;

namespace Quests {

    /// <summary>
    /// Defines events to inform subscribers when quests and objectives are added, or when they succeed or fail.
    /// It is also used by active quest as a container for context components.
    /// </summary>
    public class QuestsEnvironment : MonoBehaviour {

        public event Action<ActiveQuest, ActiveQuest.Objective> OnNewObjective, OnObjectiveSucceeded, OnObjectiveFailed;
        public event Action<ActiveQuest> OnQuestStarted, OnQuestSucceeded, OnQuestFailed;

        public C BeginQuest<C>(Quest<C> quest) where C : QuestContext {
            var context = GetComponent<C>();
            if (context != null) { return context; }
            context = gameObject.AddComponent<C>();
            gameObject.AddComponent<ActiveQuest>().Begin(quest);
            return context;
        }

        public void NewObjective(ActiveQuest quest, ActiveQuest.Objective objective) {
            Trigger(OnNewObjective, quest, objective);
        }

        public void ObjectiveSucceeded(ActiveQuest quest, ActiveQuest.Objective objective) {
            Trigger(OnObjectiveSucceeded, quest, objective);
        }

        public void ObjectiveFailed(ActiveQuest quest, ActiveQuest.Objective objective) {
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

        void Trigger(Action<ActiveQuest, ActiveQuest.Objective> action, ActiveQuest quest, ActiveQuest.Objective objective) {
            if (action != null) { action.Invoke(quest, objective); }
        }

        void Trigger(Action<ActiveQuest> action, ActiveQuest quest) {
            if (action != null) { action.Invoke(quest); }
        }
    }
}
