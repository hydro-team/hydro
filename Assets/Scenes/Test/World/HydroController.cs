using UnityEngine;
using System.Collections;
using System;
using Gestures;

public class HydroController : MonoBehaviour {

	static HydroController _instance;

	public static HydroController Instance{
		get{
			return _instance;
		}
	}

	public static event Action<GameObject,bool> HasMoved;

	//set the gesture controller
	public GameObject gestures;

	void Awake(){
		_instance = this;


		//set the gesture controller
		GesturesDispatcher dispatcher = gestures.GetComponent<GesturesDispatcher>();
		// specific gesture type
			//dispatcher.OnTapEnd += tap => DoSomethingWithEnded(tap);
		dispatcher.OnPinchEnd += sprinch => onPinchEnd(sprinch);
		dispatcher.OnSpreadEnd += sprinch => onSpreadEnd(sprinch);
		// any gesture type
			//dispatcher.OnGestureProgress += gesture => DoSomethingWithAnyOngoing(gesture);
	}

	public void Start(){
		//GesturesDispatcher.OnSpreadEnd += onSpreadEnd;
		//GesturesDispatcher.OnPinchEnd += onPinchEnd;

	}

	void onSpreadEnd(Sprinch spread){
		if(WorldManager.Instance.CanMove((Vector2)transform.position,true)){
			//FIXME
			transform.position += new Vector3(0f,0f,WorldManager.SLICE_DEPTH);
			HasMoved(this.gameObject,true);
			//TODO  call function moved in WorldManager
			//WorldManager.Instance.moved(this.gameObject, true);
		}
	}

	void onPinchEnd(Sprinch pinch){
		if (WorldManager.Instance.CanMove ((Vector2)transform.position, false)) {
			transform.position -= new Vector3 (0f, 0f, WorldManager.SLICE_DEPTH);
			HasMoved (this.gameObject, false);
			//WorldManager.Instance.moved(this.gameObject, false);
		}
	}
	

}
