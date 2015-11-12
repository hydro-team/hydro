using UnityEngine;
using Gestures;

public class IOManager : MonoBehaviour {

	static IOManager _manager;

    public GameObject gestures;
    public GameObject flowObject;

	public static IOManager InOutManager{
		get{
			return _manager;
		}
	}
	// Use this for initialization

	enum IO_State{
		IDLE,
		FLOW,
		INTERACT,
		GOING_OUT,
		GOING_IN
	}

	IO_State _currentState=IO_State.IDLE;

	void Awake(){
		_manager = this;
	}

	void Start () {
        var dispatcher = gestures.GetComponent<GesturesDispatcher>();
		dispatcher.OnGestureStart += gestureStart;
		dispatcher.OnGestureProgress += gestureProgress;
		dispatcher.OnGestureEnd += gestureEnd;

	}
	
	// Update is called once per frame
	void Update () {





	}

	void gestureStart(Gesture gesture){

		switch (gesture.Type) {
		case GestureType.TAP:
			break;
		case GestureType.SWIPE:
			swipeBegin((Swipe)gesture);
			break;
		case GestureType.PINCH:
			break;
		case GestureType.SPREAD:
			break;
		default:
			break;

		}

	}

	void gestureProgress(Gesture gesture){
		
		switch (gesture.Type) {
		case GestureType.TAP:
			break;
		case GestureType.SWIPE:
			break;
		case GestureType.PINCH:
			break;
		case GestureType.SPREAD:
			break;
		default:
			break;
			
		}
		
	}
	void gestureEnd(Gesture gesture){
		
		switch (gesture.Type) {
		case GestureType.TAP:
			break;
		case GestureType.SWIPE:
			break;
		case GestureType.PINCH:
			break;
		case GestureType.SPREAD:
			break;
		default:
			break;
			
		}
		
	}

	void tapBegin(Tap tap){
		//TODO boh?
	}

	void tapEnd(Tap tap){
		//TODO dire al mondo che cosa ho attivato e interagirci
	}


	void swipeBegin(Swipe swipe){
		//TODO instanziare il flow

	}

	void swipeProgress(Swipe swipe){
		//TODO resize del flow
	}

	void swipeEnd(Swipe swipe){
		//TODO cancellare una corrente con un altra inversa?
	}

	void pinchBegin(Sprinch pinch){
		//TODO ?
	}

	void pinchProgress(Sprinch pinch){
		//TODO dare la % al mondo (.6 = 1)
	}

	void pinchEnd(Sprinch pinch){
		//TODO dire al mondo se cancellare o se muovere, se cancella tornare indietro altrimenti concludere l'azione se possibile, altrimenti sbattere e tornare indietro(PROBLEMA DEL MONDO)
	}

	void spreadEnd(Sprinch spread){
		//TODO dire al mondo di spostarsi oppure fare animazione di 'sbattere'
	}
}
