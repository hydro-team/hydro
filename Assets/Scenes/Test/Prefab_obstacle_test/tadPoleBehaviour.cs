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
		Debug.Log ("MySprite " + spritefrog.GetComponent<SpriteRenderer>().sprite);

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
		Debug.Log (spritefrog.GetComponent<SpriteRenderer>().sprite);
		Debug.Log ("Changed sprite");
		transform.position += Vector3.up * waterlevel; 
		activable = true;
	}

	public void askJump(){
		if(activable){
			Debug.Log ("jump asked");
			anim.SetTrigger("Jump");
		}
	}

	public void OnCollisionEnter2D(Collision2D collided){
		Debug.Log (collided.gameObject.tag);
		if(collided.gameObject.tag == "Player"){
			if(!evolution){
				string feed = collided.gameObject.GetComponent<Inventory>().getInventoy(); 
				Debug.Log ("Picked " + feed); 
				if(feed == "seaweed"){
					beFeeded();
				}
			}else{
				askJump();
			}
		}
	}
}
