using UnityEngine;
using System.Collections;

public class SeaweedBehaviour : MonoBehaviour {

	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "Player"){
			bool picked = collided.gameObject.GetComponent<Inventory>().pickUp(Items.SEAWEED);
			if(picked){
				gameObject.SetActive(false);
			}
		}
	}
}
