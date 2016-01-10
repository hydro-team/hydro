using System.Collections.Generic;

namespace Quests {

    /// <summary>
    /// A quest has a name and a description that contain information on how to complete it.
    /// A quest begins with one or more initial objectives. When completed, the objectives may unlock additional objectives.
    /// With the Initialize method, a quest adds components to the QuestsEnvironment object that will be used to track the objectives' progress.
    /// </summary>
    public abstract class Quest {

        public abstract IList<QuestObjective> FirstObjectives();

        public abstract string Name();

        public abstract string Description();

        public abstract void Initialize(QuestsEnvironment environment);
    }
}
