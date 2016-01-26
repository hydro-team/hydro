using UnityEngine;
using Quests;
using System.Collections;

public class CollectorLight : MonoBehaviour {

	public QuestsEnvironment environment;

	public void checkAwake(){
		if(environment.GetComponent<TutorialQuest.Context>().lights == environment.GetComponent<TutorialQuest.Context>().getTotLights()){
			explodescreen();
		}
	}

	void explodescreen(){
		//TODO screen lights activation

		Application.LoadLevel("Level1");
	}
}
