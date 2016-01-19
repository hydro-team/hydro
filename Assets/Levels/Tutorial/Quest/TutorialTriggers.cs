using UnityEngine;
using System.Collections;
using Quests;

public class TutorialTriggers : MonoBehaviour {

	public QuestsEnvironment environment;
	public int step;

	TutorialQuest.Context context;

	void OnTriggerEnter2D ( Collider2D other){
		if (other.tag == "Player") {
			switch (step){
			case 0:
				context = environment.BeginQuest<TutorialQuest.Context>(new TutorialQuest());
				break;
			case 1:
				context.moved = true;
				break;
			case 2:
				context.pinched = true;
				break;
			case 3:
				context.outMaze = true;
				break;
			case 4:
				context.spreaded = true;
				break;
			}
		}
	}
}
