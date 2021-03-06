﻿using UnityEngine;
using Quests;
using System.Collections;

public class FeedOcto : MonoBehaviour {
	
	public QuestsEnvironment environment;
	public GameObject targetnextSlice;
	public Animator anim;
	public GameObject dialog;
	public GameObject frana;
	public GameObject d2;

	public void OnTriggerEnter2D(Collider2D collided){
		Debug.Log(collided.tag);
		WorriedFishQuest.Context context = environment.GetComponent<WorriedFishQuest.Context> ();
		if(collided.gameObject.tag == "Player" && context.octoAwake){
			Inventory inv = collided.gameObject.GetComponent<Inventory>();
			Items seaweed = inv.getInventoy(); 
			if(seaweed == Items.SEAWEED){
				Debug.Log("ItemDelivered");
				inv.freeInventory();
				context.fedOcto = true;
				anim.SetBool("MoveTo", true);
				dialog.SetActive(false);
			}
		}
	}

	public void endedAniamtion(){
		targetnextSlice.SetActive(true);
		//transform.position = Vector3.zero;
		transform.parent = targetnextSlice.transform;
		transform.position = Vector3.zero;
		transform.localScale = Vector3.down;
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
		//Invoke("activeparts", 2);
	}

	public void end(){
		//frana.SetActive(false);
		//Application.LoadLevel("end");
	}
	void activeparts(){
		d2.GetComponent<MovableObject>().enabled = true;
		d2.GetComponent<BoxCollider2D>().enabled = true;
	}
}


