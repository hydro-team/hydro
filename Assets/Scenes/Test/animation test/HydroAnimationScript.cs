using UnityEngine;
using System.Collections;


public class HydroAnimationScript : MonoBehaviour {

	public Animator animHydroSprite;
	public Animator animCamera;
	public Animator animSliceHydro;
	public GameObject cam;
	public float threshold;

	private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
		animHydroSprite.SetFloat("Speed",0f);
		rigidbody = gameObject.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody.velocity.x > 0) {
			transform.localScale = new Vector3 (-1f, 1f,1f);

		} else {
			transform.localScale = new Vector3 ( 1f,1f,1f);
		}
		if (rigidbody.velocity.sqrMagnitude > threshold) {
			animHydroSprite.SetBool("Moving",true);
		} else {
			animHydroSprite.SetBool("Moving",false);
		}
	}

	public void animCameraFarNear(bool far_near){
		animCamera.SetBool("Far_near", far_near);
		animCamera.SetTrigger("MoveCamera");
	}

	public void animCameraConfirm(){
		animCamera.SetTrigger ("Confirmed");
	}
	public void animCameraCancel(){
		animCamera.SetTrigger("Canceled");
	}

	public void animHydroFarNear(bool far_near){

		animHydroSprite.SetBool("Far_near", far_near);
		animSliceHydro.SetBool("Far_near", far_near);
		animHydroSprite.SetTrigger("SliceMove");
		animSliceHydro.SetTrigger ("Move");
		animCameraConfirm();
	}

	public void animBounce(bool far_near){
		animHydroSprite.SetBool("Far_near", far_near);
		animHydroSprite.SetTrigger("Bounce");
		animCameraCancel();
	}

	public void animMove(){
		animHydroSprite.SetTrigger("Move");
	}


}
