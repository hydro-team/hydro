using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureRecogniser : MonoBehaviour
{
	private static GestureRecogniser _recogniser;
	public static GestureRecogniser Recogniser{
		get {
//			if(_recogniser == null){
//				_recogniser = this;
//			}
			return _recogniser;
		}
	}
	List <GestureEndObserver> _endObservers = new List<GestureEndObserver>();
	List <GestureProgressObserver> _progressObservers = new List<GestureProgressObserver> ();

	GestureState state = GestureState.NEUTRAL;
	Gesture currentGesture;

	const float MAX_DISTANCE_BEFORE_SWIPE = 10f;


	// Use this for initialization
	void Awake ()
	{
		if (_recogniser == null) {
			GestureRecogniser._recogniser = this;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state) {
		case GestureState.TAP_BEGIN:
			tap ();
			break;
		case GestureState.SWIPE_BEGIN:
			swipe ();
			break;
		default:
			beginRecognition ();
			break;
		}
	
	}

	enum GestureState
	{
		SWIPE_BEGIN,
		SWIPE_END,
		PINSPR_START,
		SPREAD,
		PINCH,
		SPREAD_END,
		PINCH_END,
		PINSPR_NEUTRAL,
		TAP_BEGIN,
		NEUTRAL,
	}
	/// <summary>
	/// Begins the recognition of the current gesture.
	/// </summary>
	void beginRecognition ()
	{
		if (Input.touches.Length == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				currentGesture = new Tap (Input.touches [0].position, Time.time);
				state = GestureState.TAP_BEGIN;
			}
		}
	}

	/// <summary>
	/// Recognising a tap gesture
	/// </summary>
	void tap(){
		if (Input.touches [0].phase == TouchPhase.Ended) {
			notifyEnd();
			state = GestureState.NEUTRAL;
			return;
		}
		if (Input.touches.Length==1 && Vector2.Distance (Input.touches [0].position, ((Tap)currentGesture).Position) > MAX_DISTANCE_BEFORE_SWIPE) {
			currentGesture = new Swipe(((Tap)currentGesture).Position);
			state = GestureState.SWIPE_BEGIN;
			//Il caso limite in cui lo swipe viene riconosciuto nello stesso momento in cui finisce è escluso
			return;
		}
		//TODO pinch&Spread
	}
	/// <summary>
	/// recognising the swipe gesture
	/// </summary>
	void swipe(){
		if (Input.touches [0].phase == TouchPhase.Ended) {
			((Swipe)currentGesture).End = Input.touches[0].position;
			state = GestureState.NEUTRAL;
			notifyEnd();
		}
	}
	/// <summary>
	/// Notifies the end of a gesture to all the observers.
	/// </summary>
	void notifyEnd(){
		foreach (GestureEndObserver obs in _endObservers) {
			obs.notify(currentGesture);
		}
	}
	/// <summary>
	/// Notifies the progress ff a gesture to all the observers.
	/// </summary>
	void notifyProgress(){
		foreach (GestureProgressObserver obs in _progressObservers) {
			obs.notify(currentGesture);
		}
	}

	/// <summary>
	/// Subscribe the specified observer.
	/// </summary>
	/// <param name="observer">Observer.</param>
	public void subscribe (GestureEndObserver observer){
		_endObservers.Add (observer);
	}
}

