using UnityEngine;
using System.Collections;

public class MudFlowBehaviour : MonoBehaviour {

	BoxCollider2D coll;
	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "Player"){
			Inventory inv = collided.gameObject.GetComponent<Inventory>();
			Items shield = inv.getInventoy(); 
			if(shield == Items.MUDSHIELD){
				inv.freeInventory();
				disableCollider();
			}
		}
	}
	public void OnTriggerExit2D(Collider2D other) {
		coll.isTrigger = false;
	}
	private void disableCollider(){
		coll.isTrigger = true;
	}
}
