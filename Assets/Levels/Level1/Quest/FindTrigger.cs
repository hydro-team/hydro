using UnityEngine;
using System.Collections;
using Quests;

public class FindTrigger : MonoBehaviour {

	public QuestsEnvironment env;
	public int questStep;


	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			WorriedFishQuest.Context context = env.GetComponent<WorriedFishQuest.Context>();
			switch(questStep){
			case 0: context.fishFoundt = true;
				break;
			case 1 : context.octoFoundt = true;
				break;
			case 2 :
				context.investigate = true;
				break;

			}
		}
		this.enabled = false;
	}
}
