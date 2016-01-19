﻿using UnityEngine;
using Quests;
using System.Collections;

public class FreeFish : MonoBehaviour {

	public QuestsEnvironment environment;
	public Animator anim;
	public GameObject frana;
	
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			environment.GetComponent<WorriedFishQuest.Context>().seeFish = true;
			anim.SetTrigger("BreakRock");
		}
	}

	public void end(){
		frana.SetActive(false);
		Application.LoadLevel("end");
	}
}
