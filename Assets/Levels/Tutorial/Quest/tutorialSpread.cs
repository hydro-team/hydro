using UnityEngine;
using Quests;
using System.Collections;

public class tutorialSpread : MonoBehaviour {
	
	public QuestsEnvironment environment;
	public WorldManager wm;
	
	void Update (){
        var context = environment.GetComponent<TutorialQuest.Context>();
        if (wm.CurrentSliceIndex == 0 && context != null && context.pinched) {
			environment.GetComponent<TutorialQuest.Context>().spreaded =true;
			this.enabled = false;
			Destroy(this);
		}
	}
}

