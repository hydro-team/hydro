using UnityEngine;
using Quests;
using System.Collections;

public class FreeFish : MonoBehaviour {

	public QuestsEnvironment environment;
	public Animator anim;
	public Animator trappedF;
	public GameObject frana;
	public GameObject dialog;
	public GameObject worriedFish;

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			environment.GetComponent<WorriedFishQuest.Context>().seeFish = true;
			anim.SetTrigger("BreakRock");
			dialog.SetActive(false);
			worriedFish.SetActive(false);
			Invoke("end",3.75f);
			//anim.SetBool("RemoveRocks", true);
		}
	}




	public void end(){
		trappedF.SetBool ("Free", true);
		this.gameObject.SetActive (false);
		//frana.SetActive(false);
		//Application.LoadLevel("end");
	}
}
