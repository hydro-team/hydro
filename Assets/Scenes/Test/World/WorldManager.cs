using UnityEngine;
using System;
using System.Collections;
using Gestures;
using Sounds;
using Extensions;
using Quests;

public class WorldManager : MonoBehaviour {

    public const float SLICE_DEPTH = 1f;

    public GameObject character;
    public GesturesDispatcher gestures;
    public new Camera camera;
    public GameObject soundFacade;
    public GameObject[] slices;
    public Vector2 initialPosition;
    public int initialSlice;
    public HydroAnimationScript anim;

	bool possiblepinch;
	bool possiblespread;

    static int currentSliceIndex;

    SoundFacade sounds;
    MotionController hydroController;

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
        if (sounds == null) { throw new InvalidOperationException("Missing SoundFacade component from the soundFacade game object"); }
        hydroController = character.GetComponent<MotionController>();
        if (hydroController == null) { throw new InvalidOperationException("Missing MotionController component from the character game object"); }
        currentSliceIndex = initialSlice;
        character.transform.SetParent(slices[initialSlice].transform);
        AlignSlices();
        //        character.transform.position = new Vector3(initialPosition.x, initialPosition.y, CurrentSliceZ);
        for (int i = 0; i < slices.Length; i++) {
            if (i != initialSlice) {
                Physics2D.IgnoreLayerCollision(character.layer, slices[i].layer, true);
            }
        }

        gestures.OnTapEnd += MoveCharacter;
        gestures.OnPinchStart += ScoutNear;
        gestures.OnSpreadStart += ScoutFar;
        //TODO Manca la cancellazione della gesture 


        gestures.OnPinchEnd += MoveNear;
        gestures.OnSpreadEnd += MoveFar;
        var music = sounds.Play("/ambientali/background");
        var swipeSound = sounds["/ambientali/swype"];
        gestures.OnSwipeStart += swipe => swipeSound.Play();
        gestures.OnSwipeEnd += swipe => swipeSound.Stop();

		possiblepinch = false;
		possiblespread = false;
    }

    void AlignSlices() {
        for (int i = 0; i < slices.Length; i++) {
            slices[i].transform.position = Vector3.back * (i * SLICE_DEPTH);
            slices[i].SetActive(true);
			GameObject woda = slices [i].transform.Find ("Water").gameObject;
			alpha = woda.GetComponent<SpriteRenderer> ().color.a;
			woda.transform.position = new Vector3 (woda.transform.position.x, woda.transform.position.y, slices [i].transform.position.z + SLICE_DEPTH / 2f);
        }
    }

    void MoveCharacter(Tap tap) {
        var z = character.transform.position.z;
        var position = camera.ScreenToWorldPoint(tap.Position, z);
        hydroController.MoveTo(position);
        StartCoroutine(TapEffect(new Vector3(position.x, position.y, z)));
    }

    bool CanMoveFar() {
     /*   if (currentSliceIndex == 0) { return false; }
        var sliceFar = slices[currentSliceIndex - 1];
        var layerMask = 1 << sliceFar.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, OverlapRadius(), layerMask);
        return collider == null || collider.isTrigger;*/

		return possiblespread;
    }

    bool CanMoveNear() {
      /*  if (currentSliceIndex == slices.Length - 1) { return false; }
        var sliceNear = slices[currentSliceIndex + 1];
        var layerMask = 1 << sliceNear.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, OverlapRadius(), layerMask);
        return collider == null || collider.isTrigger;*/

		return possiblepinch;
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
			StartCoroutine(slice(false));
            RefreshCharacterCollisionStatusHack();
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
//            var position = character.transform.position;
//            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
			StartCoroutine(slice(true));
            RefreshCharacterCollisionStatusHack();
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

    void RefreshCharacterCollisionStatusHack() {
        var collider = character.GetComponent<Collider2D>();
        collider.enabled = false;
        collider.enabled = true;
    }

    void ScoutNear(Sprinch pinch) {
        Debug.Log("Scout iniziato");
        anim.animCameraFarNear(false);
    }
    void ScoutFar(Sprinch spread) {
        anim.animCameraFarNear(true);
    }

    void ScoutCanceled(Sprinch spread) {
        anim.animCameraCancel();
    }

    IEnumerator TapEffect(Vector3 position) {
        var circle = transform.FindChild("CircleIcon");
        circle.transform.position = position;
		circle.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = LayerMask.LayerToName( slices [currentSliceIndex].layer);
		circle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        circle.gameObject.SetActive(false);
        yield return null;
    }

	public float slicingTime;
	float alpha;
	IEnumerator slice(bool inout){
		SpriteRenderer woda;
		if (inout) {
			woda = CurrentSlice.transform.Find ("Water").GetComponent<SpriteRenderer> ();
		} else {
			woda = slices[currentSliceIndex+1].transform.Find ("Water").GetComponent<SpriteRenderer> ();
		}
//		if(inout)woda.color = new Color (woda.color.r, woda.color.g, woda.color.b, 0f);
		float time = Time.time;
		float iter = 0f;
		float initialz = character.transform.position.z;
		gestures.gameObject.SetActive (false);
		while (iter < slicingTime) {
			iter += Time.deltaTime;
			woda.color = new Color (woda.color.r, woda.color.g, woda.color.b, inout?iter/slicingTime*alpha: (1f-iter/slicingTime)*alpha);
			character.transform.position = new Vector3 (character.transform.position.x, character.transform.position.y, Mathf.Lerp (initialz, CurrentSlice.transform.position.z,iter/slicingTime));
			yield return null;
		}
		if (inout) {
			woda.color = new Color (woda.color.r, woda.color.g, woda.color.b, alpha);
		} else {
			woda.color = new Color (woda.color.r, woda.color.g, woda.color.b, 0f);
		}
		gestures.gameObject.SetActive (true);
	}

//	IEnumerator sliceOUT(){
//	}

	public void possiblePinch(){
		possiblepinch = true;
		possiblespread = false;
	}

	public void possibleSpread(){
		possiblepinch = false;
		possiblespread = true;
	}
	public void exitVortex(){
		possiblepinch = false;
		possiblespread = false;
	}
}
