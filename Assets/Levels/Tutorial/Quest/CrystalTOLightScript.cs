using UnityEngine;
using System.Collections;
using Quests;

public class CrystalTOLightScript : MonoBehaviour {

	public Inventory inventory;
	public QuestsEnvironment environment;
	public CollectorLight coll;
	public SpriteRenderer rend;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(inventory.item == Items.ENERGY){
				inventory.item = Items.EMPTY;
				environment.GetComponent<TutorialQuest.Context>().lights++;
				coll.checkAwake();
				rend.enabled = true;
				Destroy (this);
			}
		}
	}

}
