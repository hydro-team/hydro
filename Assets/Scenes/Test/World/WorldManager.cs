using UnityEngine;
using Gestures;

public class WorldManager : MonoBehaviour {

    public const float SLICE_DEPTH = 5f;

    public GameObject character;
    public GesturesDispatcher gestures;
    public GameObject[] slices;
    public Vector2 initialPosition;
    public int initialSlice;

    static int currentSliceIndex;

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
        currentSliceIndex = initialSlice;
        AlignSlices();
        character.transform.position = new Vector3(initialPosition.x, initialPosition.y, CurrentSliceZ);
        for (int i = 0; i < slices.Length; i++) {
            if (i != initialSlice) {
                Physics2D.IgnoreLayerCollision(character.layer, slices[i].layer, true);
            }
        }
        gestures.OnPinchEnd += MoveNear;
        gestures.OnSpreadEnd += MoveFar;
        FMOD_StudioSystem.instance.GetEvent("event:/ambientali/background").start();
        var swipeSound = FMOD_StudioSystem.instance.GetEvent("event:/ambientali/swype");
        gestures.OnSwipeStart += swipe => swipeSound.start();
        gestures.OnSwipeEnd += swipe => swipeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void AlignSlices() {
        for (int i = 0; i < slices.Length; i++) {
            slices[i].transform.position = Vector3.back * (i * SLICE_DEPTH);
        }
    }

    bool CanMoveFar() {
        if (currentSliceIndex == 0) { return false; }
        var slice = slices[currentSliceIndex - 1];
        var radius = character.GetComponent<CircleCollider2D>().radius * character.transform.localScale.x;
        var layerMask = 1 << slice.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, radius, layerMask);
        return collider == null;
    }

    bool CanMoveNear() {
        if (currentSliceIndex == slices.Length - 1) { return false; }
        var slice = slices[currentSliceIndex + 1];
        var radius = character.GetComponent<CircleCollider2D>().radius * character.transform.localScale.x;
        var layerMask = 1 << slice.layer;
        var collider = Physics2D.OverlapCircle((Vector2)character.transform.position, radius, layerMask);
        return collider == null;
    }

    void MoveFar(Sprinch spread) {
        if (CanMoveFar()) {
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, true);
            CurrentSliceIndex -= 1;
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, false);
            var position = character.transform.position;
            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
            FMOD_StudioSystem.instance.GetEvent("event:/ambientali/sliceMove").start();
        } else {
            FMOD_StudioSystem.instance.GetEvent("event:/ambientali/limitHit").start();
        }
    }

    void MoveNear(Sprinch pinch) {
        if (CanMoveNear()) {
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, true);
            CurrentSliceIndex += 1;
            Physics2D.IgnoreLayerCollision(character.layer, CurrentSlice.layer, false);
            var position = character.transform.position;
            character.transform.position = new Vector3(position.x, position.y, CurrentSliceZ);
            FMOD_StudioSystem.instance.GetEvent("event:/ambientali/sliceMove").start();
        } else {
            FMOD_StudioSystem.instance.GetEvent("event:/ambientali/limitHit").start();
        }
    }
}
