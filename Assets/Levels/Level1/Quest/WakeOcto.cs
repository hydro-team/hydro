using UnityEngine;
using System.Collections;
using Quests;

public class WakeOcto : MonoBehaviour {

	public QuestsEnvironment environment;
	public Animator OctoAnimator;
	public GameObject particle;
	public GameObject dialog; 

	void OnCollisionEnter2D (Collision2D other){

		if (other.gameObject.tag == "Flow") {
			Debug.Log ("flowCol");
			environment.GetComponent<WorriedFishQuest.Context>().octoAwake = true;
			OctoAnimator.SetBool("Awake", true);
			particle.SetActive(false);
			dialog.SetActive(true);
		}
	}
	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Flow") {
			Debug.Log ("flowT");
			environment.GetComponent<WorriedFishQuest.Context>().octoAwake = true;
			OctoAnimator.SetBool("Awake", true);
			particle.SetActive(false);
			dialog.SetActive(true);
		}
	}
}
