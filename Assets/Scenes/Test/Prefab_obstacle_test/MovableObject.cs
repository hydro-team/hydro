using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	Rigidbody2D rb;
	float speed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.magnitude > 0){
			speed = rb.velocity.magnitude;
		}
	}

	public float getSpeed(){
		return speed;
	}
}
