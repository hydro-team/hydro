using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

	public delegate void ProcessGestureEvent ( Gesture gesture);


	enum ProcessType{
		START,
		PROGRESS,
		END
	}


	public static event Action<Gesture> GestureStart ;
	public static event Action<Gesture> GestureProgress ;
	public static event Action<Gesture> GestureEnd ;

//	public static void SetStartDispatcher (Action<Gesture> newDisp){
//		GestureStart = newDisp;
//	}
//
//	public static void SetProgressDispatcher (Action<Gesture> newDisp){
//		GestureProgress = newDisp;
//	}

//	List <ProcessGestureEvent> _startObservers = new List<ProcessGestureEvent> ();
//	List <ProcessGestureEvent> _endObservers = new List<ProcessGestureEvent>();
//	List <ProcessGestureEvent> _progressObservers = new List<ProcessGestureEvent> ();

	private GestureState _state = GestureState.NEUTRAL;
	Gesture currentGesture;

	const float MAX_DISTANCE_BEFORE_SWIPE = 20f;

	public GestureState CurrentState{
		get{
			return _state;
		}
	}

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
//		Debug.Log (GestureProgress != null?GestureProgress.GetInvocationList().Length.ToString():"Nobody here");

		switch (_state) {
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

	public enum GestureState
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
				if (_state == GestureState.NEUTRAL) {
					notifyObservers(ProcessType.START);
				}
				_state = GestureState.TAP;

			}
		}
		if (Input.touches.Length > 1) {
			currentGesture = new Sprinch(Input.touches[0].position,Input.touches[1].position);
			_state = GestureState.SPRINCH;
		}
	}

	/// <summary>
	/// Recognising a tap gesture
	/// </summary>
	void tap(){
		if (Input.touches.Length ==0 ){//||Input.touches [0].phase == TouchPhase.Ended) {
			notifyObservers(ProcessType.END);
			return;
		}
		if (Input.touches.Length==1 && Vector2.Distance (Input.touches [0].position, ((Tap)currentGesture).Position) > MAX_DISTANCE_BEFORE_SWIPE) {
			currentGesture = new Swipe(((Tap)currentGesture).Position);
			if (_state == GestureState.TAP) {
				notifyObservers(ProcessType.START);
			}

			_state = GestureState.SWIPE;

			//Il caso limite in cui lo swipe viene riconosciuto nello stesso momento in cui finisce è escluso
			return;
		}
		//TODO pinch&Spread
		//FIXME
		if (Input.touches.Length > 1) {
			currentGesture = new Sprinch(((Tap)currentGesture).Position,Input.touches[1].position);
			_state = GestureState.SPRINCH;
		}
	}
	/// <summary>
	/// recognising the swipe gesture
	/// </summary>
	void swipe(){
		//FIXME

//		Debug.Log ("Swiping");
		if (Input.touches.Length > 1) {
			currentGesture = new Sprinch(((Swipe)currentGesture).Start,Input.touches[1].position);
			_state = GestureState.SPRINCH;
			return;
		}
		if (Input.touches.Length < 1) {
			notifyObservers(ProcessType.END);
			return;
		}
		if (Input.touches [0].phase == TouchPhase.Ended) {
			((Swipe)currentGesture).End = Input.touches[0].position;
			notifyObservers(ProcessType.END);
			return;
		}
		if (Input.touches [0].phase == TouchPhase.Moved) {
			((Swipe)currentGesture).End = Input.touches[0].position;
			notifyObservers(ProcessType.PROGRESS);
			return;
		}
	}

	void sprinch(){
//		Debug.Log ("Sprinching");
		if (Input.touches.Length < 2) {
			notifyObservers(ProcessType.END);
			return;
		}

		if ( Input.touches [0].phase == TouchPhase.Ended || Input.touches [1].phase == TouchPhase.Ended) {
			Vector2[] points = new Vector2[2]{Input.touches[0].position,Input.touches[1].position};
			((Sprinch)currentGesture).EndPoints =points;
			notifyObservers(ProcessType.END);
			return;
		}
		if (Input.touches [0].phase == TouchPhase.Moved || Input.touches [0].phase == TouchPhase.Moved) {
			Gesture.GestureType sprtype = ((Sprinch)currentGesture).Type;

			((Sprinch)currentGesture).EndPoints = new Vector2[2]{Input.touches[0].position,Input.touches[1].position};

			if(sprtype == Gesture.GestureType.SPRINCH && currentGesture.Type!=Gesture.GestureType.SPRINCH){
				notifyObservers(ProcessType.START);
			}
			notifyObservers(ProcessType.PROGRESS);
			return;
		}
	}
	/// <summary>
	/// Notifies the end of a gesture to all the observers.
	/// </summary>


	void notifyObservers ( ProcessType phase){
		switch (phase) {
		case ProcessType.START:
			if(GestureStart!=null) GestureStart(currentGesture);
			break;
		case ProcessType.PROGRESS:
			if(GestureProgress!=null){GestureProgress(currentGesture);
//				Debug.Log("GestureProgress is: "+GestureProgress.ToString());
			}
			break;
		case ProcessType.END:
			if(GestureEnd!=null)GestureEnd(currentGesture);
			_state = GestureState.NEUTRAL;
			break;
		}
	}
	/// <summary>
	/// Notifies the progress ff a gesture to all the observers.
	/// </summary>
//	void notifyProgress(){
//		GestureProgress (currentGesture);
//	}
//	void notifyStart(){
//		GestureStart (currentGesture);
////		foreach (ProcessGestureEvent pr in _startObservers) {
////			pr(currentGesture);
////		}
//	}
//
//	void notifyEnd(){
//		foreach (ProcessGestureEvent pr in _endObservers) {
//			pr(currentGesture);
//		}
//		_state = GestureState.NEUTRAL;
//	}

//	/// <summary>
//	/// Subscribe the specified observer to the end of a gesture.
//	/// </summary>
//	/// <param name="observer">Observer.</param>
//	public void subscribeEnd (ProcessGestureEvent observer){
//		_endObservers.Add (observer);
//	}
//	/// <summary>
//	/// Subscribe the specified observer to the progress of a gesture.
//	/// </summary>
//	/// <param name="observer">Observer.</param>
//	public void subscribeProgress(ProcessGestureEvent observer){
//		_progressObservers.Add (observer);
//	}
//	/// <summary>
//	/// Subscribe the specified observer to the start of a gesture.
//	/// </summary>
//	/// <param name="observer">Observer.</param>
//	public void subscribeStart(ProcessGestureEvent observer){
//		_startObservers.Add (observer);
//	}



}

