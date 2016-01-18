using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Quests;

public class TutorialQuest : Quest {

	// Use this for initialization
	public override string Name()
	{
		return "First step";
	}
	
	public override string Description()
	{
		return "Find a way out";
	}
	
	public override IList<QuestObjective> FirstObjectives(){
		return new List<QuestObjective> { new GoAhead() };
	}
	
	public override void Initialize(QuestsEnvironment environment) {
		environment.gameObject.AddComponent<Context>();
	}
	
	public class Context:MonoBehaviour{
		public bool moved;
		public bool pinched;
		public bool outMaze;
		public bool spreaded;
		public int lights;
		public const int TOT_LIGHTS=4;
	}
	
	class GoAhead : QuestObjective{
		public override string Description()
		{
			return "Move toward the exit";
		}
		
		public override bool IsOptional(){
			return false;
		}
		
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().moved ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new UsePassage() };
		}
	}

	class UsePassage: QuestObjective{
		public override string Description(){
			return "Use the passage you foundt";
		}

		public override bool IsOptional(){
			return false;
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().pinched ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new ExitTheDarkMaze() };
		}
	}

	class ExitTheDarkMaze:QuestObjective{
		public override string Description(){
			return "The way ahead is dark: use the light to find the other end";
		}
		
		public override bool IsOptional(){
			return false;
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().outMaze ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new ExitTheDarkMaze() };
		}
	}
	class SpreadOut :QuestObjective{
		public override string Description(){
			return "Find a way out of the cave";
		}
		
		public override bool IsOptional(){
			return false;
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			return environment.GetComponent<Context>().spreaded? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> { new LightCrystals() };
		}
	}

	class LightCrystals:QuestObjective{


		public override string Description(){
			return "Find an energy source to light up the four crystals";
		}
		
		public override bool IsOptional(){
			return false;
		}
		public override ProgressStatus StatusIn(QuestsEnvironment environment) {
			Context context = environment.GetComponent<Context> ();
			return context.lights == Context.TOT_LIGHTS ? ProgressStatus.SUCCEEDED : ProgressStatus.ONGOING;
		}
		public override IList<QuestObjective> NextObjectives() {
			return new List<QuestObjective> {  };
		}
	}
}
