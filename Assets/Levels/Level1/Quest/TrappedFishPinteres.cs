using UnityEngine;
using System.Collections;
using Gestures;

public class TrappedFishPinteres : MonoBehaviour {

	public GameObject pointOfInterest;
	public GesturesDispatcher dispatcher;

	void OnTriggerEnter2D(Collider2D other){
		dispatcher.gameObject.SetActive (false);
		Invoke ("DisablePinterest" , 2f);

	}

	void DisablePinterest(){
		dispatcher.gameObject.SetActive (true);
		pointOfInterest.SetActive (false);
	}

}
