using UnityEngine;
using System.Collections;

public class TimedFishingWire : MonoBehaviour {
	/// <summary>
	/// The time the rod will stay immerse in water in seconds.
	/// </summary>
	public float immersetime;
	/// <summary>
	/// The time the rod will stay emerse in water in seconds.
	/// </summary>
	public float emersetime;
	/// <summary>
	/// The time that must be still waited for a change of state.	
	/// </summary>
	private float curtime;
	/// <summary>
	/// The state of the rod
	/// </summary>
	private bool immerse;
	/// <summary>
	/// The rod is emerseheight of the objct to be hidden by the player when the .
	/// </summary>
	public float waterlevel;
	/// <summary>
	/// The Vector of the movement of the object to be hidden by the player when emerse.
	/// </summary>
	private Vector3 over;
	/// <summary>
	/// The reference of the object wire.
	/// </summary>
	public GameObject wire;
	/// <summary>
	/// The reference of the object rod.
	/// </summary>
	public GameObject rod;


	void Start () {
		immerse = true;
		curtime = immersetime;
		over = new Vector3(0f,waterlevel,0f);
	}
	

	void Update () {
		if(curtime >= 0){
			curtime = curtime - Time.deltaTime;
		}else{
			//Debug.Log("inverse");
			if(immerse){
				emerse();

			}else{
				immer();
				//activateRod();
				//activateWire();
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

	/// <summary>
	/// Disactivates constrants on the rod.
	/// </summary>
	private void activateRod(){
		rod.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
	}
	/// <summary>
	/// Disactivates constrants on the wire.
	/// </summary>
	private void activateWire(){
		wire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
	}

	/// <summary>
	/// method called form outside to forcefully make the rod emerse.
	/// </summary>
	public void emergencyExit(){
		emerse();
	}
	/// <summary>
	/// bring the rod outside the water.
	/// </summary>
	private void emerse(){
		immerse = false;
		curtime = emersetime;
		//TODO animation to move over the water
		gameObject.transform.position += over;
	}
	/// <summary>
	/// IImmerse the rod
	/// </summary>
	private void immer(){
		immerse = true;
		curtime = immersetime;
		//TODO animation to move inside the water
		gameObject.transform.position -= over;
	}
}
