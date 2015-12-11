using UnityEngine;
using System.Collections;

public class tadPoleBehaviour : MonoBehaviour {
	
	bool evolution;
	bool activable;
	bool inJump;
	public float waterlevel;
	public float speed;
	Animator anim;
	Rigidbody2D rb;
	public Sprite frogsp;
	public GameObject dialog;
	public GameObject spritefrog;

	void Start(){
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void beFeeded(){
		evolution = true;
		anim.SetTrigger("Transformation");
		dialog.SetActive(false);
	}

	public void endJump(){
		transform.position += Vector3.right*10;
	}

	public void endTransformation(){
		spritefrog.GetComponent<SpriteRenderer>().sprite = frogsp;
		transform.position += Vector3.up * waterlevel; 
		activable = true;
	}

	public void askJump(){
		if(activable){
			anim.SetTrigger("Jump");
		}
	}

	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "Player"){
			if(!evolution){
				Inventory inv = collided.gameObject.GetComponent<Inventory>();
				Items feed = inv.getInventoy(); 
				if(feed == Items.SEAWEED){
					inv.freeInventory();
					beFeeded();
				}
			}else{
				askJump();
			}
		}
	}
}
