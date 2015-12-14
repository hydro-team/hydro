using UnityEngine;
using System.Collections;

public class Grid_Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "destructablerock"){
			gameObject.SetActive(false);
		}
	}
}
