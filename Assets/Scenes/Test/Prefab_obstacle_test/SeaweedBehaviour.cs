using UnityEngine;
using System.Collections;

public class SeaweedBehaviour : MonoBehaviour {

	Sprite s;

	void Start(){
		s = GetComponent<SpriteRenderer>().sprite;
	}

	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "Player"){
			bool picked = collided.gameObject.GetComponent<Inventory>().pickUp(Items.SEAWEED, s);
			if(picked){
				gameObject.SetActive(false);
			}
		}
	}
}
