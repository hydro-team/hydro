using System;
using System.Linq;
using System.Collections.Generic;

namespace Quests {

    /// <summary>
    /// An objective has a list of zero or more next objectives, that are added to the quest required objectives.
    /// It has a description that helps players to figure out the task they must accomplish.
    /// An objective must be able to determine whether it is still uncomplete, it succeeded or failed by analyzing the context in the QuestsEnvironment.
    /// An objective may be optional: it does not make the whole quest fail if it fails.
    /// </summary>
    public abstract class QuestObjective<C> where C : QuestContext {

        public readonly Func<C, string> descriptionIn;
        public readonly Func<C, ProgressStatus> statusIn;
        public readonly bool isOptional;
        public readonly IEnumerable<QuestObjective<C>> nextObjectives;

        public QuestObjective(Func<C, string> description, Func<C, ProgressStatus> status, bool optional = false, IEnumerable<QuestObjective<C>> next = null) {
            descriptionIn = description;
            statusIn = status;
            isOptional = optional;
            nextObjectives = next != null ? next : Enumerable.Empty<QuestObjective<C>>();
        }

        public QuestObjective(string description, Func<C, ProgressStatus> status, bool optional = false, IEnumerable<QuestObjective<C>> next = null) {
            descriptionIn = context => description;
            statusIn = status;
            isOptional = optional;
            nextObjectives = next != null ? next : Enumerable.Empty<QuestObjective<C>>();
        }

        public static ProgressStatus SucceedsWhen(bool condition) {
            return condition ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
        }

        public static ProgressStatus FailsWhen(bool condition) {
            return condition ? ProgressStatus.FAILED : ProgressStatus.ONGOING;
        }
    }
}
