using UnityEngine;
using Quests;
using System.Collections;

public class FindSeaweed : MonoBehaviour {

	public QuestsEnvironment environment;
	
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			environment.GetComponent<WorriedFishQuest.Context>().collectedSeaweed = true;
			this.enabled = false;
		}
	}
}
