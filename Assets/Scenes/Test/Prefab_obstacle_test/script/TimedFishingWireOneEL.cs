using UnityEngine;
using System.Collections;

public class TimedFishingWireOneEL : MonoBehaviour {
	/// <summary>
	/// This variable contains the time in second that the rod will stay inside the water.
	/// </summary>
	public float immersetime;
	/// <summary>
	/// This variable contains the time in second that the rod will stay outside the water.
	/// </summary>
	public float emersetime;
	/// <summary>
	/// This variable contains the current time in second untill the next change of state.
	/// </summary>
	private float curtime;
	/// <summary>
	/// This variable contains the state of the rod: true if the rod is immerse, false otherwise.
	/// </summary>
	private bool immerse;
	/// <summary>
	/// This variable contains the height at which the rod must be put when it emerge in order to hide it from the player.
	/// </summary>
	public float waterlevel;
	/// <summary>
	/// This variable contains the vector to be summed or subtracted to the anchor of the rod to hide it from player when it is merse.
	/// </summary>
	private Vector2 over;
	//public GameObject rod;
	
	void Start () {
		immerse = true;
		curtime = immersetime;
		over = new Vector2(0f,waterlevel);
		//rod = this.gameObject;
	}
	
	/// <summary>
	/// This method perform the task of controlling the countdown and decide if it is the tiem to change state.
	/// </summary>
	void Update () {
		if(curtime >= 0){
			curtime = curtime - Time.deltaTime;
		}else{
			//Debug.Log("inverse");
			if(immerse){
				emerse();

			}else{
				immer();
			}
		}
		/*if(!immerse && (rod.transform.localRotation.z <= 5f || rod.transform.localRotation.z>= 355f)){
			rod.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			Invoke("activateRod", 0.5f);
		}
		if(!immerse && (wire.transform.localRotation.z <= 5f || wire.transform.localRotation.z>= 355f)){
			wire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			Invoke("activateWire", 0.5f);
		}*/
	}

	/*/// <summary>
	/// This method perform the task of reactivate the degree of freedom of the rod.
	/// </summary>
	private void activateRod(){
		rod.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
	}*/
	/// <summary>
	/// This method perform the task of bring the rod out of the water even if the countdown is not ended yet.
	/// It is called from other classes to signal the rod to emerge.
	/// </summary>
	public void emergencyExit(){
		emerse();
	}

	/// <summary>
	/// This method perform the task of bringing the od out of the water. Namely out of the view of the player.
	/// </summary>
	private void emerse(){
		Debug.Log("emerse");
		immerse = false;
		curtime = emersetime;
		//TODO animation to move over the water
		//gameObject.transform.position += over;
		gameObject.GetComponent<HingeJoint2D>().connectedAnchor += over;
	}
	/// <summary>
	/// This method perform the task of immerse the rod in the water.
	/// </summary>
	private void immer(){
		immerse = true;
		curtime = immersetime;
		//TODO animation to move inside the water
		//gameObject.transform.position -= over;
		gameObject.GetComponent<HingeJoint2D>().connectedAnchor -= over;
	}
}
