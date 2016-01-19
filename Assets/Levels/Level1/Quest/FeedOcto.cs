using UnityEngine;
using Quests;
using System.Collections;

public class FeedOcto : MonoBehaviour {
	
	public QuestsEnvironment environment;
	public GameObject targetnextSlice;
	public Animator anim;

	public void OnTriggerEnter2D(Collider2D collided){
		Debug.Log(collided.tag);
		if(collided.gameObject.tag == "Player"){
			Inventory inv = collided.gameObject.GetComponent<Inventory>();
			Items seaweed = inv.getInventoy(); 
			if(seaweed == Items.SEAWEED){
				Debug.Log("ItemDelivered");
				inv.freeInventory();
				environment.GetComponent<WorriedFishQuest.Context>().fedOcto = true;
				anim.SetBool("MoveTo", true);
			}
		}
	}

	public void endedAniamtion(){
		targetnextSlice.SetActive(true);
		transform.position = Vector3.zero;
		transform.parent = targetnextSlice.transform;
		transform.position = Vector3.zero;
		moveToSlice1(transform.GetChild(0).gameObject);
	}

	void moveToSlice1(GameObject part){
		if(part.transform.childCount == 0){
			part.layer = 12;
			part.GetComponent<SpriteRenderer>().sortingLayerName = "Slice1";
		}else{
			for(int i = 0; i < part.transform.childCount; i++){
				moveToSlice1(part.transform.GetChild(i).gameObject);
			}
			part.layer = 12;
			part.GetComponent<SpriteRenderer>().sortingLayerName = "Slice1";
		}
	}
}


