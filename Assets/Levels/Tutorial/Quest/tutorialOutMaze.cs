using UnityEngine;
using Quests;
using System.Collections;

public class tutorialOutMaze : MonoBehaviour {

	public QuestsEnvironment environment;

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			environment.GetComponent<TutorialQuest.Context>().outMaze =true;
			this.enabled = false;
			Destroy(this);
		}
	}
}
