using UnityEngine;
using System.Collections;


public class HydroAnimationScript : MonoBehaviour {

    public Animator animHydroSprite;
    public Animator animCamera;
    public Animator animSliceHydro;
    public GameObject cam;
    public float threshold;

    public SpriteRenderer[] sprites;

    private Rigidbody2D body;

    void Start() {
        animHydroSprite.SetFloat("Speed", 0f);
        body = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update() {
        LookTowardMovementDirection();
        SetMotionState();
    }

    void LookTowardMovementDirection() {
        var scale = transform.localScale;
        var cameraScale = animCamera.transform.parent.localScale;
        if (body.velocity.x > threshold) {
            scale.x = -Mathf.Abs(scale.x);
            cameraScale.x = -Mathf.Abs(cameraScale.x);
            transform.localScale = scale;
            animCamera.transform.parent.localScale = cameraScale;
        } else if (body.velocity.x < -threshold) {
            scale.x = Mathf.Abs(scale.x);
            cameraScale.x = Mathf.Abs(cameraScale.x);
            transform.localScale = scale;
            animCamera.transform.parent.localScale = cameraScale;
        }
    }

    public void CreateCurrent() {
        animHydroSprite.SetTrigger("Current");
    }

    void SetMotionState() {
        var moving = body.velocity.sqrMagnitude > threshold;
        animHydroSprite.SetBool("Moving", moving);
    }

    public void animCameraFarNear(bool far_near) {
        animCamera.SetBool("Far_near", far_near);
        animCamera.SetTrigger("MoveCamera");
    }

    public void animCameraConfirm() {
        animCamera.SetTrigger("Confirmed");
    }
    public void animCameraCancel() {
        animCamera.SetTrigger("Canceled");
    }

    public void animHydroFarNear(bool far_near) {

        if (far_near) {
            animHydroSprite.SetTrigger("SliceIN");
        } else {
            animHydroSprite.SetTrigger("SliceOUT");
        }
    }

    public void animBounce(bool far_near) {
        animHydroSprite.SetBool("Far_near", far_near);
        animHydroSprite.SetTrigger("Bounce");
        animCameraCancel();
    }

    public void animMove() {
        animHydroSprite.SetTrigger("Move");
    }

    public void switchSlice(GameObject slice) {
        foreach (SpriteRenderer spr in sprites) {
            spr.sortingLayerName = LayerMask.LayerToName(slice.layer);
        }
    }
}
