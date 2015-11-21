using UnityEngine;
using System.Collections;

public class BreackableRock : MonoBehaviour {

	public  float life;
	public float coefficient;
	
	// Use this for initialization
	void Start () {
		life = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(life <= 0){
			//apply animation berackup
			
			//TODO Rockmanager must deactivate this object in order to reuse it in other slice
			//RockManager.Instance.deacivaterock(this.gameObject);
			Destroy(this.gameObject);
		}
	}
	
	public void OnCollisionEnter2D(Collision2D collided){
		Debug.Log ("BreackableRock"+collided.collider.name);
		//compute the impact force
		MovableObject mo = collided.collider.GetComponent<MovableObject>();
		if(mo != null){
			float vel = mo.getSpeed();//collided.collider.attachedRigidbody.velocity.magnitude;
			Debug.Log ("BreackableRock"+"velocity " + vel);
			float height = (vel*vel) / (2*collided.collider.attachedRigidbody.gravityScale);
			Debug.Log ("BreackableRock"+"height " + height);
			float differencecinematicenergy = Mathf.Abs((float)(collided.collider.attachedRigidbody.gravityScale*collided.collider.attachedRigidbody.mass*height)-(float)(0.5* collided.collider.attachedRigidbody.mass * vel*vel)) * Mathf.Pow(10, 8);
			Debug.Log ("BreackableRock"+"DKE " + differencecinematicenergy);
			float impact_force = differencecinematicenergy / height;
			Debug.Log ("BreackableRock"+"ImpactForce " + impact_force);
			
			//Damage calculation
			float damage = impact_force * coefficient;
			Debug.Log ("BreackableRock"+"Damage " + damage);
			life = life - damage;
		}
	}
}
