using UnityEngine;
using System.Collections;

public class HydroAnimationScript : MonoBehaviour {

	public Animator animHydroSprite;
	public Animator animCamera;
	public Animator animSliceHydro;
	public GameObject cam;
	// Use this for initialization
	void Start () {
		animHydroSprite.SetFloat("Speed",0f);
	}
	
	// Update is called once per frame
	void Update () {

		animHydroSprite.SetFloat("Speed", gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude);
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
