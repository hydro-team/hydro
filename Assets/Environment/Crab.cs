using UnityEngine;
using System.Collections;

public class Crab : MonoBehaviour {
	public Crab otherCrab;
	public Transform Arrival;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" ) {
			col.transform.position = otherCrab.Arrival.position ;
			otherCrab.gameObject.SetActive(false);
			Invoke("wakeUp", 4f);

		}
	}

	void wakeUp(){
		otherCrab.gameObject.SetActive(true);
	}
}
