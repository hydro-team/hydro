using System.Collections.Generic;
using System.Linq;

namespace Quests {

    /// <summary>
    /// A quest has a name and a description that contain information on how to complete it.
    /// A quest begins with one or more initial objectives. When completed, the objectives may unlock additional objectives.
    /// With the Initialize method, a quest adds components to the QuestsEnvironment object that will be used to track the objectives' progress.
    /// </summary>
    public abstract class Quest<C> where C : QuestContext {

        public readonly string name;
        public readonly string description;
        public readonly IEnumerable<QuestObjective<C>> firstObjectives;

        public Quest(string name, string description, IEnumerable<QuestObjective<C>> startingWith = null) {
            this.name = name;
            this.description = description;
            this.firstObjectives = startingWith != null ? startingWith : Enumerable.Empty<QuestObjective<C>>();
        }

        public static IEnumerable<QuestObjective<C>> Objectives<C>(params QuestObjective<C>[] objectives) where C : QuestContext { return objectives; }
    }
}
