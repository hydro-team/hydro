using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

	public GameObject[] slices;
	public static float MAX_SLICE_X_DIMENSION;
	public static float SLICE_DEPTH = 5f;
	public Vector2 startPosition;
	public int startSlice;

	static WorldManager _instance;
	public static WorldManager Instance{
		get{
			return _instance;
		}
	}


	static int _currentSlice;

	public int CurrentSlice {
		get{
			return _currentSlice;
		}
		set{
			_currentSlice = value;

			if (value <0){
				_currentSlice = 0;
				return;
			}
			if(value >= slices.Length){
				_currentSlice = slices.Length-1;
				return;
			}
			_currentSlice = value;
		}

	}

	void Awake(){
		_instance = this;
	}

	public void AlignSlices(){
		for (int i=0; i<slices.Length; i++) {
			slices[i].transform.position = new Vector3(0f,0f,(float)i * -SLICE_DEPTH);
		}
	}

	// Use this for initialization
	void Start () {
		//FIXME
		AlignSlices ();
		HydroController.Instance.gameObject.transform.position = new Vector3(startPosition.x,startPosition.y,(float)startSlice * -SLICE_DEPTH);
		HydroController.HasMoved += moved;
	}

	public bool CanMove(Vector2 position,bool deep){
		Debug.Log ("Called can move");
		int layerindex=_currentSlice;
		if (deep && layerindex - 1 > -1) {
			layerindex -=1;
		}
		if (!deep && layerindex + 1 < slices.Length) {
			layerindex+=1;
		}
		return !Physics2D.Raycast (position, Vector2.zero, 0f, layerindex);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void moved(GameObject character, bool deep){
		//TODO
		Physics.IgnoreLayerCollision (character.layer,slices[_currentSlice].layer,true);
		CurrentSlice = (deep? -1 :+1)+CurrentSlice;
		Physics.IgnoreLayerCollision (character.layer,slices[_currentSlice].layer,false);


	}
}
