using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Quests {

    /// <summary>
    /// Represents the status of a ongoing quest.
    /// Instantiate it by adding it as a component to the QuestsEnvironment, then call the Begin method.
    /// A quest can be interrupted while running by calling the method Terminate: it will succeed if only optional objectives are left.
    /// </summary>
    public class ActiveQuest : MonoBehaviour {

        public Quest quest;

        QuestsEnvironment environment;
        ProgressStatus status = ProgressStatus.INACTIVE;
        IList<QuestObjective> currentObjectives = new List<QuestObjective>();
        IList<QuestObjective> succeededObjectives = new List<QuestObjective>();
        IList<QuestObjective> failedObjectives = new List<QuestObjective>();

        public void Begin(Quest quest) {
            if (status != ProgressStatus.INACTIVE) { return; }
            this.quest = quest;
            environment = GetComponent<QuestsEnvironment>();
            NewQuest();
            foreach (QuestObjective objective in quest.FirstObjectives()) {
                NewObjective(objective);
            }
        }

        public void Terminate() {
            if (status != ProgressStatus.ONGOING) { return; }
            foreach (QuestObjective remaining in new List<QuestObjective>(currentObjectives)) {
                ObjectiveFailed(remaining);
            }
            if (OnlyOptionalObjectivesFailed()) {
                QuestSucceeded();
            } else {
                QuestFailed();
            }
        }

        public IEnumerable<QuestObjective> CurrentObjectives() {
            return currentObjectives;
        }

        public IEnumerable<QuestObjective> SucceededObjectives() {
            return succeededObjectives;
        }

        public IEnumerable<QuestObjective> FailedObjectives() {
            return failedObjectives;
        }

        bool OnlyOptionalObjectivesFailed() {
            return failedObjectives.All(objective => objective.IsOptional());
        }

        void Update() {
            if (status != ProgressStatus.ONGOING) { return; }
            foreach (QuestObjective objective in new List<QuestObjective>(currentObjectives)) {
                var objectiveStatus = objective.StatusIn(environment);
                if (objectiveStatus == ProgressStatus.FAILED) {
                    ObjectiveFailed(objective);
                    if (!objective.IsOptional()) {
                        QuestFailed();
                        return;
                    }
                } else if (objectiveStatus == ProgressStatus.SUCCEEDED) {
                    ObjectiveSucceeded(objective);
                    foreach (QuestObjective next in objective.NextObjectives()) {
                        NewObjective(next);
                    }
                }
            }
            if (currentObjectives.Count == 0) { QuestSucceeded(); }
        }

        void NewQuest() {
            quest.Initialize(environment);
            status = ProgressStatus.ONGOING;
            environment.QuestStarted(this);
        }

        void QuestSucceeded() {
            status = ProgressStatus.SUCCEEDED;
            environment.QuestSucceeded(this);
        }

        void QuestFailed() {
            status = ProgressStatus.FAILED;
            environment.QuestFailed(this);
        }

        void NewObjective(QuestObjective objective) {
            currentObjectives.Add(objective);
            environment.NewObjective(this, objective);
        }

        void ObjectiveSucceeded(QuestObjective objective) {
            currentObjectives.Remove(objective);
            succeededObjectives.Add(objective);
            environment.ObjectiveSucceeded(this, objective);
        }

        void ObjectiveFailed(QuestObjective objective) {
            currentObjectives.Remove(objective);
            failedObjectives.Add(objective);
            environment.ObjectiveFailed(this, objective);
        }
    }
}
