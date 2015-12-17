using UnityEngine;
using System;
using Gestures;
using Sound;

public class WorldManager : MonoBehaviour {

    public const float SLICE_DEPTH = 1f;

    public GameObject character;
    public GesturesDispatcher gestures;
    public GameObject soundFacade;
    public GameObject[] slices;
    public Vector2 initialPosition;
    public int initialSlice;
	public HydroAnimationScript anim;

    static int currentSliceIndex;

    SoundFacade sounds;

    public int CurrentSliceIndex {
        get { return currentSliceIndex; }
        set {
            currentSliceIndex = value;
            if (value < 0) { currentSliceIndex = 0; }
            if (value >= slices.Length) { currentSliceIndex = slices.Length - 1; }
        }
    }

    public GameObject CurrentSlice {
        get { return slices[currentSliceIndex]; }
    }

    public float CurrentSliceZ {
        get { return currentSliceIndex * -SLICE_DEPTH; }
    }

    void Awake() {
        sounds = soundFacade.GetComponent<SoundFacade>();
        if (sounds == null) { throw new InvalidOperationException("Missing SoundFacade component game object assigned to soundFacade"); }
        currentSliceIndex = initialSlice;
		character.transform.SetParent (slices [initialSlice].transform);
        AlignSlices();
//        character.transform.position = new Vector3(initialPosition.x, initialPosition.y, CurrentSliceZ);
        for (int i = 0; i < slices.Length; i++) {
            if (i != initialSlice) {
                Physics2D.IgnoreLayerCollision(character.layer, slices[i].layer, true);
            }
        }


		gestures.OnPinchStart += ScoutNear;
		gestures.OnSpreadStart += ScoutFar;
//TODO Manca la cancellazione della gesture 


        gestures.OnPinchEnd += MoveNear;
        gestures.OnSpreadEnd += MoveFar;
        var music = sounds.Play("/ambientali/background");
        var swipeSound = sounds["/ambientali/swype"];
        gestures.OnSwipeStart += swipe => swipeSound.Play();
        gestures.OnSwipeEnd += swipe => swipeSound.Stop();
    }

    void AlignSlices() {
        for (int i = 0; i < slices.Length; i++) {
            slices[i].transform.position = Vector3.back * (i * SLICE_DEPTH);
			slices[i].SetActive(true);
        }
    }

    bool CanMoveFar() {
        if (currentSliceIndex == 0) { return false; }
        var sliceFar = slices[currentSliceIndex - 1];
        var layerMask = 1 << sliceFar.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, OverlapRadius(), layerMask);
        return collider == null || collider.isTrigger;
    }

    bool CanMoveNear() {
        if (currentSliceIndex == slices.Length - 1) { return false; }
        var sliceNear = slices[currentSliceIndex + 1];
        var layerMask = 1 << sliceNear.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, OverlapRadius(), layerMask);
        return collider == null || collider.isTrigger;
    }

    float OverlapRadius() {
        var scale = character.transform.localScale;
        var radiusScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y));
        return character.GetComponent<CircleCollider2D>().radius * radiusScale;
    }

    void MoveFar(Sprinch spread) {
        if (CanMoveFar()) {
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, true);
            CurrentSliceIndex -= 1;
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, false);
            var position = character.transform.position;
            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
            sounds.Play("/ambientali/sliceMove");

			anim.animCameraConfirm();
			anim.animHydroFarNear(true);
			anim.switchSlice(CurrentSlice);

        } else {
            sounds.Play("/ambientali/limitHit");
			
			anim.animCameraCancel();
			anim.animBounce(true);

        }
    }

    void MoveNear(Sprinch pinch) {
        if (CanMoveNear()) {
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, true);
            CurrentSliceIndex += 1;
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, false);
            var position = character.transform.position;
            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
            sounds.Play("/ambientali/sliceMove");

			
			anim.animCameraConfirm();
			anim.animHydroFarNear(false);
			anim.switchSlice(CurrentSlice);

        } else {
            sounds.Play("/ambientali/limitHit");

			anim.animCameraCancel();
			anim.animBounce(false);
        }
    }
	void ScoutNear(Sprinch pinch) {
		Debug.Log ("Scout iniziato");
		anim.animCameraFarNear(false);
	}
	void ScoutFar(Sprinch spread) {
		anim.animCameraFarNear(true);
	}

	void ScoutCanceled(Sprinch spread) {
		anim.animCameraCancel();
	}
}
