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
	List <GestureObserver> observers = new List<GestureObserver>();

	GestureState state = GestureState.NEUTRAL;
	Gesture currentGesture;

	const float tapMaxDragDistance = 10f;
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

	void beginRecognition ()
	{
		if (Input.touches.Length == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				currentGesture = new Tap (Input.touches [0].position, Time.time);
				state = GestureState.TAP_BEGIN;
			}
		}
	}
	void tap(){
		if (Input.touches [0].phase == TouchPhase.Ended) {
			notifyWatchers();
			state = GestureState.NEUTRAL;
			return;
		}
		if (Input.touches.Length==1 && Vector2.Distance (Input.touches [0].position, ((Tap)currentGesture).Position) > tapMaxDragDistance) {
			currentGesture = new Swipe(((Tap)currentGesture).Position);
			state = GestureState.SWIPE_BEGIN;
			//Il caso limite in cui lo swipe viene riconosciuto nello stesso momento in cui finisce è escluso
			return;
		}
		//TODO pinch&Spread
	}

	void swipe(){
		if (Input.touches [0].phase == TouchPhase.Ended) {
			((Swipe)currentGesture).End = Input.touches[0].position;
			state = GestureState.NEUTRAL;
			notifyWatchers();
		}
	}
	void notifyWatchers(){
		//TODO
		foreach (GestureObserver obs in observers) {
			obs.notify(currentGesture);
		}
	}

	public void subscribe (GestureObserver observer){
		observers.Add (observer);
	}
}

