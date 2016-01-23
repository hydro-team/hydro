using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quests {

    /// <summary>
    /// Represents the status of a ongoing quest.
    /// Instantiate it by adding it as a component to the QuestsEnvironment, then call the Begin method.
    /// A quest can be interrupted while running by calling the method Terminate: it will succeed if only optional objectives are left.
    /// </summary>
    public class ActiveQuest : MonoBehaviour {

        string questName;
        string description;
        QuestsEnvironment environment;
        ProgressStatus status = ProgressStatus.INACTIVE;
        Func<IEnumerable<ActiveQuest.Objective>> currentObjectives;
        IList<ActiveQuest.Objective> succeededObjectives = new List<ActiveQuest.Objective>();
        IList<ActiveQuest.Objective> failedObjectives = new List<ActiveQuest.Objective>();
        Action update;
        Action terminate;

        public void Begin<C>(Quest<C> quest) where C : QuestContext {
            if (status != ProgressStatus.INACTIVE) { return; }
            questName = quest.name;
            description = quest.description;
            environment = GetComponent<QuestsEnvironment>();
            InitializePolymorphicContext(quest);
        }

        void InitializePolymorphicContext<C>(Quest<C> quest) where C : QuestContext {
            status = ProgressStatus.ONGOING;
            var objectives = new List<QuestObjective<C>>();
            Func<QuestObjective<C>, Objective> hideType = objective => Objective.From(objective, () => environment.GetComponent<C>());
            Func<QuestObjective<C>, Objective> freeze = objective => Objective.FreezeFrom(objective, environment.GetComponent<C>());
            currentObjectives = () => {
                var context = environment.GetComponent<C>();
                return objectives.Select(hideType);
            };
            Action<QuestObjective<C>> newObjective = objective => {
                objectives.Add(objective);
                environment.NewObjective(this, hideType(objective));
            };
            Action<QuestObjective<C>> objectiveSucceeded = objective => {
                objectives.Remove(objective);
                var frozenObjective = freeze(objective);
                succeededObjectives.Add(frozenObjective);
                environment.ObjectiveSucceeded(this, frozenObjective);
            };
            Action<QuestObjective<C>> objectiveFailed = objective => {
                objectives.Remove(objective);
                var frozenObjective = freeze(objective);
                failedObjectives.Add(frozenObjective);
                environment.ObjectiveFailed(this, frozenObjective);
            };
            update = () => {
                var context = environment.GetComponent<C>();
                foreach (QuestObjective<C> objective in new List<QuestObjective<C>>(objectives)) {
                    var objectiveStatus = objective.statusIn(context);
                    if (objectiveStatus == ProgressStatus.FAILED) {
                        objectiveFailed(objective);
                        if (!objective.isOptional) {
                            QuestFailed();
                            return;
                        }
                    } else if (objectiveStatus == ProgressStatus.SUCCEEDED) {
                        objectiveSucceeded(objective);
                        foreach (QuestObjective<C> next in objective.nextObjectives) {
                            newObjective(next);
                        }
                    }
                }
                if (objectives.Count == 0) { QuestSucceeded(); }
            };
            terminate = () => {
                foreach (var remaining in new List<QuestObjective<C>>(objectives)) {
                    objectiveFailed(remaining);
                }
                if (OnlyOptionalObjectivesFailed()) {
                    QuestSucceeded();
                } else {
                    QuestFailed();
                }
            };
            environment.QuestStarted(this);
            foreach (var objective in quest.firstObjectives) {
                newObjective(objective);
            }
        }

        public void Terminate() {
            if (status != ProgressStatus.ONGOING) { return; }
            terminate();
        }

        public string Name { get { return questName; } }

        public string Description { get { return description; } }

        public IEnumerable<ActiveQuest.Objective> CurrentObjectives() {
            return currentObjectives();
        }

        public IEnumerable<ActiveQuest.Objective> SucceededObjectives() {
            return succeededObjectives;
        }

        public IEnumerable<ActiveQuest.Objective> FailedObjectives() {
            return failedObjectives;
        }

        public ProgressStatus Status { get { return status; } }

        bool OnlyOptionalObjectivesFailed() {
            return failedObjectives.All(objective => objective.isOptional);
        }

        void Update() {
            if (status != ProgressStatus.ONGOING) { return; }
            update();
        }

        void QuestSucceeded() {
            status = ProgressStatus.SUCCEEDED;
            environment.QuestSucceeded(this);
        }

        void QuestFailed() {
            status = ProgressStatus.FAILED;
            environment.QuestFailed(this);
        }

        public struct Objective {

            public readonly Func<string> description;
            public readonly Func<ProgressStatus> status;
            public readonly bool isOptional;

            Objective(Func<string> description, Func<ProgressStatus> status, bool isOptional) {
                this.description = description;
                this.status = status;
                this.isOptional = isOptional;
            }

            Objective(string description, ProgressStatus status, bool isOptional) {
                this.description = () => description;
                this.status = () => status;
                this.isOptional = isOptional;
            }

            public static Objective From<C>(QuestObjective<C> objective, Func<C> context) where C : QuestContext {
                return new Objective(() => objective.descriptionIn(context()), () => objective.statusIn(context()), objective.isOptional);
            }

            public static Objective FreezeFrom<C>(QuestObjective<C> objective, C context) where C : QuestContext {
                return new Objective(objective.descriptionIn(context), objective.statusIn(context), objective.isOptional);
            }

            public string Description { get { return description(); } }

            public ProgressStatus Status { get { return status(); } }
        }
    }
}
