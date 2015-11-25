using UnityEngine;
using System.Collections;

public class RodBehaviour : MonoBehaviour {
	/// <summary>
	/// The object attached to the rod.
	/// </summary>
	public GameObject wedged;
	/// <summary>
	/// The limit on the weight the rod can sustain before being ejected by the water.
	/// </summary>
	public float weightlimit;


	void Start () {
		wedged = null;
	}
	

	void Update () {
		if(wedged != null){
			checkMass();
		}

	}

	/// <summary>
	/// Raises the collision enter2 d event. attach the gameobject to the rod if no other object is already attached
	/// </summary>
	/// <param name="coll">Collision2D of the other Gameobject that hit the rod</param>
	void OnCollisionEnter2D(Collision2D coll){

		if(wedged == null){
			Debug.Log (coll.collider.name);
			wedged = coll.gameObject;
			Debug.Log(wedged);
			wedged.AddComponent<HingeJoint2D>();
			wedged.GetComponent<HingeJoint2D>().connectedBody = this.gameObject.GetComponent<Rigidbody2D>();
			wedged.GetComponent<HingeJoint2D>().anchor = Vector2.up;
		}
	}
	/// <summary>
	/// Checks the mass attached to the rod and if is too much ask for ejection of the rod.
	/// </summary>
	private void checkMass(){
		if(wedged.tag == "Player" || wedged.GetComponent<Rigidbody2D>().mass >= weightlimit){
			gameObject.GetComponentInParent<TimedFishingWire>().emergencyExit();
			GameObject.Destroy(wedged.GetComponent<HingeJoint2D>());
			//TODO Respawn the wedged
			wedged = null;
		}
	}
}
