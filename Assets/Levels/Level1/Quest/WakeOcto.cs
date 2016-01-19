using UnityEngine;
using System.Collections;
using Quests;

public class WakeOcto : MonoBehaviour {

	public QuestsEnvironment environment;
	public Animator OctoAnimator;

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Flow") {
			environment.GetComponent<WorriedFishQuest.Context>().octoAwake = true;
			OctoAnimator.SetBool("Awake", true);
		}
	}
}
