using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Quests;

public class TutorialQuest : Quest<TutorialQuest.Context> {

    public TutorialQuest() : base(
        name: "Awakening",
        description: "Find a way out",
        startingWith: Objectives(new GoAhead())) { }

    public class Context : QuestContext {
        public bool moved;
        public bool pinched;
        public bool outMaze;
        public bool spreaded;
        public int lights;
        public const int TOT_LIGHTS = 3;
    }

    class GoAhead : QuestObjective<Context> {
        public GoAhead() : base(
            description: "Move toward the exit",
            status: context => SucceedsWhen(context.moved),
            next: Objectives(new UsePassage())) { }
    }

    class UsePassage : QuestObjective<Context> {
        public UsePassage() : base(
            description: "Use the passage you found",
            status: context => SucceedsWhen(context.pinched),
            next: Objectives(new ExitTheDarkMaze())) { }
    }

    class ExitTheDarkMaze : QuestObjective<Context> {
        public ExitTheDarkMaze() : base(
            description: "The way ahead is dark: use the light to find the other end",
            status: context => SucceedsWhen(context.outMaze),
            next: Objectives(new SpreadOut())) { }
    }

    class SpreadOut : QuestObjective<Context> {
        public SpreadOut() : base(
            description: "Find a way out of the cave",
            status: context => SucceedsWhen(context.spreaded),
            next: Objectives(new LightCrystals())) { }
    }

    class LightCrystals : QuestObjective<Context> {
        public LightCrystals() : base(
            description: context => {
                if (context.lights == Context.TOT_LIGHTS) { return "Find an energy source to light up the three crystals"; }
                return string.Format("Find an energy source to light up the three crystals ({0} left)", Context.TOT_LIGHTS - context.lights);
            }, status: context => SucceedsWhen(context.lights == Context.TOT_LIGHTS)) { }
    }
}
