using UnityEngine;
using System.Collections;
using CameraBehaviour;
using Quests;

public class WorriedFishQuestTrigger : MonoBehaviour {

	public Animator animToSet;
	public PointOfInterest pinterest;
	public QuestsEnvironment environment;


	void OnTriggerEnter2D ( Collider2D other ) {
		if (other.tag == "Player") {
			//TODO Pop bolle con emozioni
			environment.BeginQuest<WorriedFishQuest.Context> (new WorriedFishQuest ()); 
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Player") {
			pinterest.gameObject.SetActive (false);
			animToSet.SetBool ("roam", true);
		}
	}

}
