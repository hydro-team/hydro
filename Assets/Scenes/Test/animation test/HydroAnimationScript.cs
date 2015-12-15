using UnityEngine;
using System.Collections;


public class HydroAnimationScript : MonoBehaviour {

    private static readonly Vector3 DEFAULT_SCALE = new Vector3(1f, 1f, 1f);
    private static readonly Vector3 FLIPPED_SCALE = new Vector3(-1f, 1f, 1f);

    public Animator animHydroSprite;
	public Animator animCamera;
	public Animator animSliceHydro;
	public GameObject cam;
	public float threshold;

	public SpriteRenderer[] sprites;

	private Rigidbody2D rigidbody;

	void Start () {
		animHydroSprite.SetFloat("Speed",0f);
		rigidbody = gameObject.GetComponent<Rigidbody2D> ();

	}

	void Update () {
        LookTowardMovementDirection();
        SetMotionState();
	}

    void LookTowardMovementDirection() {
        if (rigidbody.velocity.x > threshold) {
            transform.localScale = FLIPPED_SCALE;
            animCamera.transform.parent.localScale = FLIPPED_SCALE;
        } else if (rigidbody.velocity.x < -threshold) {
            transform.localScale = DEFAULT_SCALE;
            animCamera.transform.parent.localScale = DEFAULT_SCALE;
        }
    }

    void SetMotionState() {
        var moving = rigidbody.velocity.sqrMagnitude > threshold;
        animHydroSprite.SetBool("Moving", moving);
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

	public void switchSlice(GameObject slice){
		foreach (SpriteRenderer spr in sprites) {
			spr.sortingLayerName = LayerMask.LayerToName(slice.layer);
			Debug.Log("Switched"
			          );
		}
	}
}
