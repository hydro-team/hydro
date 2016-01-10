using System.Collections.Generic;

namespace Quests {

    /// <summary>
    /// An objective has a list of zero or more next objectives, that are added to the quest required objectives.
    /// It has a description that helps players to figure out the task they must accomplish.
    /// An objective must be able to determine whether it is still uncomplete, it succeeded or failed by analyzing the context in the QuestsEnvironment.
    /// An objective may be optional: it does not make the whole quest fail if it fails.
    /// </summary>
    public abstract class QuestObjective {

        public abstract IList<QuestObjective> NextObjectives();

        public abstract string Description();

        public abstract ProgressStatus StatusIn(QuestsEnvironment environment);

        public abstract bool IsOptional();
    }
}
