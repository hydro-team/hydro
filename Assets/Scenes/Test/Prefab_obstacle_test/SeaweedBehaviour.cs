using UnityEngine;
using System.Collections;

public class SeaweedBehaviour : MonoBehaviour {

	public void OnCollisionEnter2D(Collision2D collided){
		Debug.Log (collided.gameObject.tag);
		if(collided.gameObject.tag == "Player"){
			bool picked = collided.gameObject.GetComponent<Inventory>().pickUp("seaweed");
			Debug.Log ("Picked " + picked);
			if(picked){
				gameObject.SetActive(false);
			}
		}
	}
}
