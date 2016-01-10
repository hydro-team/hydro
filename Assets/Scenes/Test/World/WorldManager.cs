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
    public QuestsEnvironment environment;
    public GameObject[] slices;
    public Vector2 initialPosition;
    public int initialSlice;
    public HydroAnimationScript anim;

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

        environment.OnQuestStarted += started => Debug.Log("New quest: " + started.quest.Name());
        environment.OnQuestSucceeded += succeeded => Debug.Log("Quest succeeded: " + succeeded.quest.Name());
        environment.OnQuestFailed += failed => Debug.Log("Quest failed: " + failed.quest.Name());
        environment.OnNewObjective += (quest, objective) => Debug.Log("New objective: " + objective.Description());
        environment.OnObjectiveSucceeded += (quest, objective) => Debug.Log("Objective succeeded: " + objective.Description());
        environment.OnObjectiveFailed += (quest, objective) => Debug.Log("Objective failed: " + objective.Description());
    }

    void AlignSlices() {
        for (int i = 0; i < slices.Length; i++) {
            slices[i].transform.position = Vector3.back * (i * SLICE_DEPTH);
            slices[i].SetActive(true);
        }
    }

    void MoveCharacter(Tap tap) {
        var z = character.transform.position.z;
        var position = camera.ScreenToWorldPoint(tap.Position, z);
        hydroController.MoveTo(position);
        StartCoroutine(TapEffect(new Vector3(position.x, position.y, z)));
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
            var position = character.transform.position;
            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
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
        circle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        circle.gameObject.SetActive(false);
        yield return null;
    }
}
