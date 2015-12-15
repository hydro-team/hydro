using UnityEngine;
using System.Collections;

public class Crab : MonoBehaviour {
	public Crab otherCrab;
	public Transform Arrival;
	public SpriteRenderer Pinchers;
	bool canTransport = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (canTransport && col.tag == "Player" ) {
			col.transform.position = otherCrab.Arrival.position ;
			otherCrab.use();

		}
	}

	public void use(){
		enabled = false;
		Pinchers.enabled = false;
		canTransport = false;
		Invoke("wakeUp", 4f);
	}


	void wakeUp(){
		enabled = true;
		canTransport = true;
		Pinchers.enabled = true;

	}

}
