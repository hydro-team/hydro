using UnityEngine;
using Quests;
using System.Collections;

public class tutorialVortex : MonoBehaviour {

	public QuestsEnvironment environment;
	public WorldManager wm;
	
	void Update (){
		if (wm.CurrentSliceIndex == 1) {
			environment.GetComponent<TutorialQuest.Context>().pinched =true;
			this.enabled = false;
			Destroy(this);
		}
	}
}
