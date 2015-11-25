using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {
	/// <summary>
	/// the rigidbody of this object
	/// </summary>
	Rigidbody2D rb;
	/// <summary>
	/// The speed of the gameobject.
	/// </summary>
	float speed;


	void Start () {
		rb = GetComponent<Rigidbody2D>();
		speed = 0;
	}
	
	/// <summary>
	/// set the speed (on impact it keep the last speed memorized before the impact)
	/// </summary>
	void Update () {
		if(rb.velocity.magnitude > 0){
			speed = rb.velocity.magnitude;
		}
	}
	/// <summary>
	/// Gets the speed.
	/// </summary>
	/// <returns>The speed.</returns>
	public float getSpeed(){
		return speed;
	}
	
}
