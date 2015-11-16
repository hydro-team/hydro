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

	void Awake(){
		_instance = this;
	}

	public void Start(){
		GesturesDispatcher.OnSpreadEnd += onSpreadEnd;
		GesturesDispatcher.OnPinchEnd += onPinchEnd;

	}

	void onSpreadEnd(Sprinch spread){
		if(WorldManager.Instance.CanMove((Vector2)transform.position,true)){
			//FIXME
			transform.position += new Vector3(0f,0f,WorldManager.SLICE_DEPTH);
			HasMoved(this.gameObject,true);
			//TODO  call function moved in WorldManager
		}
	}

	void onPinchEnd(Sprinch pinch){
		if (WorldManager.Instance.CanMove ((Vector2)transform.position, false)) {
			transform.position -= new Vector3 (0f, 0f, WorldManager.SLICE_DEPTH);
			HasMoved (this.gameObject, false);
		}
	}
	

}
