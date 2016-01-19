using UnityEngine;
using System.Collections;
using Quests;

public class CrystalTOLightScript : MonoBehaviour {

	public Inventory inventory;
	QuestsEnvironment environment;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(inventory.item == Items.ENERGY){
				inventory.item = Items.EMPTY;
				environment.GetComponent<TutorialQuest.Context>().lights++;
			}
		}
	}
}
