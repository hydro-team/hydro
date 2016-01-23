using UnityEngine;
using System.Collections.Generic;
using Quests;
using System;

public class WorriedFishQuest : Quest<WorriedFishQuest.Context> {

    public WorriedFishQuest() : base(
        name: "Lost Friend",
        description: "Find the fish's friend and save him",
        startingWith: Objectives(new FindFriend())) { }

    public class Context : QuestContext {
        public bool fishFoundt;
        public bool octoFoundt;
        public bool octoAwake;
        public bool collectedSeaweed;
        public bool fedOcto;
        public bool seeFish;
        public bool investigate;
    }

    class FindFriend : QuestObjective<Context> {
        public FindFriend() : base(
            description: "Find the fish's lost friend",
            status: context => SucceedsWhen(context.fishFoundt),
            next: Objectives(new FindHelp())) { }
    }

    class FindHelp : QuestObjective<Context> {
        public FindHelp() : base(
            description: "Find someone to move the rocks",
            status: context => SucceedsWhen(context.octoFoundt),
            next: Objectives(new WakeOctopus())) { }
    }

    class WakeOctopus : QuestObjective<Context> {
        public WakeOctopus() : base(
            description: "This octopus might be helpful. Wake him up",
            status: context => SucceedsWhen(context.octoAwake),
            next: Objectives(new FeedOctopus())) { }
    }

    class FeedOctopus : QuestObjective<Context> {
        public FeedOctopus() : base(
            description: "The Octopus is weak. Find something he can eat to regain strength",
            status: context => SucceedsWhen(context.fedOcto),
            next: Objectives(new GoAndSee())) { }
    }

    class GoAndSee : QuestObjective<Context> {
        public GoAndSee() : base(
            description: "Go back to the trapped fish",
            status: context => SucceedsWhen(context.seeFish),
            next: Objectives(new Investigate())) { }
    }

    class Investigate : QuestObjective<Context> {
        public Investigate() : base(
            description: "This colorful thing seems poisonous. Go and investigate.",
            status: context => SucceedsWhen(context.investigate)) { }
    }
}
