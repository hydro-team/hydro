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

	const float MAX_DISTANCE_BEFORE_SWIPE = 20f;


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
		case GestureState.TAP:
			tap ();
			break;
		case GestureState.SWIPE:
			swipe ();
			break;
		case GestureState.SPRINCH:
			sprinch();
			break;
		default:
			beginRecognition ();
			break;
		}
	
	}

	enum GestureState
	{
		SWIPE,
		SPRINCH,
		SPREAD,
		PINCH,
		SPREAD_END,
		PINCH_END,
		PINSPR_NEUTRAL,
		TAP,
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
				state = GestureState.TAP;
			}
		}
	}

	/// <summary>
	/// Recognising a tap gesture
	/// </summary>
	void tap(){
		if (Input.touches [0].phase == TouchPhase.Ended) {
			notifyEnd();
			return;
		}
		if (Input.touches.Length==1 && Vector2.Distance (Input.touches [0].position, ((Tap)currentGesture).Position) > MAX_DISTANCE_BEFORE_SWIPE) {
			currentGesture = new Swipe(((Tap)currentGesture).Position);
			state = GestureState.SWIPE;
			//Il caso limite in cui lo swipe viene riconosciuto nello stesso momento in cui finisce è escluso
			return;
		}
		//TODO pinch&Spread
		//FIXME
		if (Input.touches.Length > 1) {
			currentGesture = new Sprinch(((Tap)currentGesture).Position,Input.touches[1].position);
			state = GestureState.SPRINCH;
		}
	}
	/// <summary>
	/// recognising the swipe gesture
	/// </summary>
	void swipe(){
		//FIXME
		if (Input.touches.Length > 1) {
			currentGesture = new Sprinch(((Tap)currentGesture).Position,Input.touches[1].position);
			state = GestureState.SPRINCH;
			return;
		}
		if (Input.touches [0].phase == TouchPhase.Ended) {
			((Swipe)currentGesture).End = Input.touches[0].position;
			notifyEnd();
		}
		if (Input.touches [0].phase == TouchPhase.Moved) {
			notifyProgress();
		}
	}

	void sprinch(){
		if (Input.touches [0].phase == TouchPhase.Ended || Input.touches [1].phase == TouchPhase.Ended) {
			Vector2[] points = new Vector2[2]{Input.touches[0].position,Input.touches[1].position};
			((Sprinch)currentGesture).End =points;
			notifyEnd();
		}
		if (Input.touches [0].phase == TouchPhase.Moved || Input.touches [0].phase == TouchPhase.Moved) {
			((Sprinch)currentGesture).CurrentPoints = new Vector2[2]{Input.touches[0].position,Input.touches[1].position};
			notifyProgress();
		}
	}
	/// <summary>
	/// Notifies the end of a gesture to all the observers.
	/// </summary>
	void notifyEnd(){
		foreach (GestureEndObserver obs in _endObservers) {
			obs.notifyEnd(currentGesture);
		}
		state = GestureState.NEUTRAL;
	}
	/// <summary>
	/// Notifies the progress ff a gesture to all the observers.
	/// </summary>
	void notifyProgress(){
		foreach (GestureProgressObserver obs in _progressObservers) {
			obs.notifyProgress(currentGesture);
		}
	}

	/// <summary>
	/// Subscribe the specified observer.
	/// </summary>
	/// <param name="observer">Observer.</param>
	public void subscribeEnd (GestureEndObserver observer){
		_endObservers.Add (observer);
	}

	public void subscribeProgress(GestureProgressObserver observer){
		_progressObservers.Add (observer);
	}
}

