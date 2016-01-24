using UnityEngine;
using System.Collections;

public class SeaweedBehaviour : MonoBehaviour {

	SpriteRenderer s;

	void Start(){
		s = GetComponent<SpriteRenderer>();

	}

	public void OnTriggerEnter2D(Collider2D collided){
		if(collided.gameObject.tag == "Player"){
			bool picked = collided.gameObject.GetComponent<Inventory>().pickUp(Items.SEAWEED, s.sprite, s.color);
			if(picked){
				gameObject.SetActive(false);
			}
		}
	}
}
