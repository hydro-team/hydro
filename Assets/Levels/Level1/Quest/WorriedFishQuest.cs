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
		public bool octoAwake;
		public bool collectedSeaweed;
		public bool fedOcto;
		public bool seeFish;
		public bool investigate;
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
			return new List<QuestObjective> { new WakeOctopus() };
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().octoFoundt ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}

	class WakeOctopus : QuestObjective {
		public override string Description(){
			return "This octopus might be helpful. Wake him up";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			//TODO
			return new List<QuestObjective> { new FeedOctopus() };
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().octoAwake ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
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
			return new List<QuestObjective> { new GoAndSee() };
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().fedOcto ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}

	class GoAndSee : QuestObjective {
		public override string Description(){
			return "Go back to the trapped fish";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			//TODO
			return new List<QuestObjective> { new Investigate() };
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().seeFish ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}

	class Investigate : QuestObjective {
		public override string Description(){
			return "This colorful thing seems poisonous. Go and investigate.";
		}

		public override bool IsOptional(){
			return false;
		}
		public override IList<QuestObjective> NextObjectives() {
			//TODO
			return new List<QuestObjective> {};
		}

		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().investigate ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
	}
}
