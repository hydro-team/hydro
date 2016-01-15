using UnityEngine;
using Saves;

public class BreackableRock : MonoBehaviour {

	/// <summary>
	/// duration of the rock
	/// </summary>
	public  float life;
	/// <summary>
	/// corrective coefficient depending by the resistance of the mateial (t be trigger manually)
	/// </summary>
	public float coefficient;
    public SaveContext saves;
    public string rockName;

    BreakableObject state;
	
	// Use this for initialization
	void Awake () {
        state = saves.GetOrCreate<BreakableObject>(rockName);
        if (state.broken) { gameObject.SetActive(false); }
        //life = 10f;
    }
	
	/// <summary>
	/// This method perform the task of cheching that the object is still active.
	/// </summary>
	void Update () {
		
		if(life <= 0){
			//apply animation berackup

			//TODO Rockmanager must deactivate this object in order to reuse it in other slice
			//RockManager.Instance.deacivaterock(this.gameObject);
//			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// This method perform the task of computing the damage dealt by the impact of a movable object 
	/// and reduce the life of this object.
	/// </summary>
	/// <param name="collided"> 
	/// the collision2D component of the object that hit this object
	/// </param>
	/// <returns>
	/// 
	/// </returns>
	public void OnCollisionEnter2D(Collision2D collided){
		Debug.Log ("BreackableRock"+collided.collider.name);
		//compute the impact force
		MovableObject mo = collided.collider.GetComponent<MovableObject>();
		if(mo != null){
			/*float vel = mo.getSpeed();//collided.collider.attachedRigidbody.velocity.magnitude;
			Debug.Log ("BreackableRock"+"velocity " + vel);
			float height = (vel*vel) / (2f*collided.collider.attachedRigidbody.gravityScale);
			Debug.Log ("BreackableRock"+"height " + height);
			float differencecinematicenergy = Mathf.Abs((float)(collided.collider.attachedRigidbody.gravityScale*collided.collider.attachedRigidbody.mass*height)-(float)(0.5* collided.collider.attachedRigidbody.mass * vel*vel)) * (float)Mathf.Pow(10, 8);
			Debug.Log ("BreackableRock"+"DKE " + differencecinematicenergy);
			float impact_force = differencecinematicenergy / height;
			Debug.Log ("BreackableRock"+"ImpactForce " + impact_force);*/
			
			//Damage calculation
//			float damage = impact_force * coefficient;
			float damage= 0;
			if(mo.getSpeed() > coefficient){
				damage = 1f;
			}
			Debug.Log ("BreackableRock"+"Damage " + damage);
			life = life - damage;
			if(life <=0){
                state.broken = true;
				GetComponent<Collider2D>().enabled = false;
				foreach(BreakableRockEffects brEff in transform.GetComponentsInChildren<BreakableRockEffects>()){
					brEff.BreakRock( collided.relativeVelocity*collided.rigidbody.mass);
				}
			}
		}
	}
}
