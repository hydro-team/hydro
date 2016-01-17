using UnityEngine;
using System.Collections.Generic;
using Quests;
using System;

public class WorriedFishQuest : Quest {

	public override string Name()
	{
		return "Lost Friend";
	}

	public override string Description()
	{
		return "Find the fish's friend and save him";
	}

	public override IList<QuestObjective> FirstObjectives(){
		return new List<QuestObjective> { new FindFriend() };
	}

	public override void Initialize(QuestsEnvironment environment) {
		environment.gameObject.AddComponent<Context>();
	}

	public class Context:MonoBehaviour{
		public bool fishFoundt;
		public bool octoFoundt;
		public bool collectedSeaweed;
		public bool fedOcto;
	}

	class FindFriend : QuestObjective{
		public override string Description()
		{
			return "Find the fish's lost friend";
		}

		public override bool IsOptional(){
			return false;
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().fishFoundt ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new FindHelp() };
		}
	}

	class FindHelp : QuestObjective{
		public override string Description(){
			return "Find someone to move the rocks";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new FeedOctopus() };
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().octoFoundt ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}

	class FeedOctopus : QuestObjective{
		public override string Description(){
			return "The Octopus is weak. Find something he can eat to regain strength";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			//TODO
			return new List<QuestObjective> { new FeedOctopus() };
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().fedOcto ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}

	class GoAndSee : QuestObjective {
		public override string Description(){
			return "The Octopus is weak. Find something he can eat to regain strength";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			//TODO
			return new List<QuestObjective> { new FeedOctopus() };
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().fedOcto ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}
}
