using UnityEngine;
using Quests;
using System.Collections;

public class FeedOcto : MonoBehaviour {
	
	public QuestsEnvironment environment;
	public GameObject targetnextSlice;

	public void OnTriggerEnter2D(Collider2D collided){
		if(collided.gameObject.tag == "Player"){
			Inventory inv = collided.gameObject.GetComponent<Inventory>();
			Items seaweed = inv.getInventoy(); 
			if(seaweed == Items.SEAWEED){
				inv.freeInventory();
				environment.GetComponent<WorriedFishQuest.Context>().fedOcto = true;
			}
		}
	}

	public void endedAniamtion(){
		transform.parent = targetnextSlice.transform;
		transform.position = Vector3.zero;
		moveToSlice1(transform.GetChild(0).gameObject);
	}

	void moveToSlice1(GameObject part){
		if(part.transform.childCount == 0){
			part.GetComponent<SpriteRenderer>().sortingLayerName = "Slice1";
		}else{
			for(int i = 0; i < part.transform.childCount; i++){
				moveToSlice1(part.transform.GetChild(i).gameObject);
			}
			part.GetComponent<SpriteRenderer>().sortingLayerName = "Slice1";
		}
	}
}


