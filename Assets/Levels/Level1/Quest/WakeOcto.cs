using UnityEngine;
using System.Collections;
using Quests;
using Sounds;

public class WakeOcto : MonoBehaviour {

	public QuestsEnvironment environment;
    public GameObject soundFacade;
	public Animator OctoAnimator;
	public GameObject particle;
	public GameObject dialog; 
	public InterestingObject []glows;

    SoundFacade sounds;

    void Awake() {
        sounds = soundFacade.GetComponent<SoundFacade>();
    }

	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Flow") {
			Debug.Log ("flowT");
			environment.GetComponent<WorriedFishQuest.Context>().octoAwake = true;
			OctoAnimator.SetBool("Awake", true);
			foreach(InterestingObject io in glows){
				io.used = true;
			}
			particle.SetActive(false);
			dialog.SetActive(true);
            sounds.Play("/oggetti/pipes");
		}
	}
}
