using UnityEngine;
using System.Collections;
using Quests;

public class CrystalTOLightScript : MonoBehaviour {

	public Inventory inventory;
	public QuestsEnvironment environment;
	public CollectorLight coll;
	public SpriteRenderer rend;
	public SpriteRenderer crystalRend;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if(inventory.item == Items.ENERGY){
				inventory.item = Items.EMPTY;
				environment.GetComponent<TutorialQuest.Context>().lights++;
				coll.checkAwake();
				crystalRend.color = Color.white;
				rend.enabled = true;
				Destroy (this);
			}
		}
	}

}
