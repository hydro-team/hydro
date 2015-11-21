using UnityEngine;
using System.Collections;

public class SmallRock : MonoBehaviour {

	public  float life;
	private Rigidbody2D rb;
	public float speed= 0;
	public float coefficient;

	// Use this for initialization
	void Start () {
		life = 5f;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.magnitude > 0){speed = rb.velocity.magnitude;}

		if(life <= 0){
			//apply animation berackup

			//TODO Rockmanager must deactivate this object in order to reuse it in other slice
			//RockManager.Instance.deacivaterock(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	public void OnCollisionEnter2D(Collision2D collider){
		//Debug.Log ("SmallRock"+collider.collider.name);
		//compute the impact force
		float vel = speed;
		//Debug.Log ("SmallRock"+"velocity " + vel);
		float height = (vel*vel) / (2*rb.gravityScale);
		//Debug.Log ("SmallRock"+"height " + height);
		float differencecinematicenergy = Mathf.Abs((float)(rb.gravityScale*rb.mass*height)-(float)(0.5* rb.mass * vel*vel)) * Mathf.Pow(10, 8);
		//Debug.Log ("SmallRock"+"DKE " + differencecinematicenergy);
		float impact_force = differencecinematicenergy / height;
		//Debug.Log ("SmallRock"+"ImpactForce " + impact_force);

		//Damage calculation
		float damage = impact_force * coefficient;
		//Debug.Log ("SmallRock"+"Damage " + damage);
		life = life - damage;

	}
}
