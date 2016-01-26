using UnityEngine;
using Quests;
using System.Collections;

public class tutorialSpread : MonoBehaviour {
	
	public QuestsEnvironment environment;
	public WorldManager wm;
	
	void Update (){
		if (wm.CurrentSliceIndex == 0 && environment.GetComponent<TutorialQuest.Context>().pinched) {
			environment.GetComponent<TutorialQuest.Context>().spreaded =true;
			this.enabled = false;
			Destroy(this);
		}
	}
}

