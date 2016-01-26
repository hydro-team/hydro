using UnityEngine;
using Quests;
using System.Collections;

public class tutorialBegin : MonoBehaviour {
	
	public QuestsEnvironment environment;
	
	
	void OnTriggerEnter2D ( Collider2D other ) {
		if (other.tag == "Player") {
			//TODO Pop bolle con emozioni
			environment.BeginQuest<TutorialQuest.Context> (new TutorialQuest ()); 
		}
	}
	
	void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Player") {
			environment.GetComponent<TutorialQuest.Context>().moved =true;
			this.enabled = false;
			Destroy(this);
		}
	}
}
