using UnityEngine;
using Quests;
using System;
using System.Collections.Generic;

public class FrogQuest : Quest<FrogQuest.Context> {

    public FrogQuest() : base(
        name: "Jump Over",
        description: "Help the tadpole to become a frog",
        startingWith: Objectives(new CollectSeaweed())) { }

    public class Context : QuestContext {
        public bool collectedSeaweed = false;
        public bool feedTadpole = false;
        public bool jumpedOver = false;
    }

    class CollectSeaweed : QuestObjective<Context> {
        public CollectSeaweed() : base(
            description: "Find and collect the seaweed",
            status: context => SucceedsWhen(context.collectedSeaweed),
            next: Objectives(new FeedTadpole())) { }
    }

    class FeedTadpole : QuestObjective<Context> {
        public FeedTadpole() : base(
            description: "Feed the tadpole with the seaweed",
            status: context => SucceedsWhen(context.feedTadpole),
            next: Objectives(new JumpOver())) { }
    }

    class JumpOver : QuestObjective<Context> {
        public JumpOver() : base(
            description: "Ask the grown frog for help",
            status: context => SucceedsWhen(context.jumpedOver)) { }
    }
}
