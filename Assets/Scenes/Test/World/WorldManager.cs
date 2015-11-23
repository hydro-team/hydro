using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

	public HydroController characterController;
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



	static int _currentSliceIndex;

	public int CurrentSliceIndex {
		get{
			return _currentSliceIndex;
		}
		set{
			_currentSliceIndex = value;

			if (value <0){
				_currentSliceIndex = 0;
				return;
			}
			if(value >= slices.Length){
				_currentSliceIndex = slices.Length-1;
				return;
			}
			_currentSliceIndex = value;
		}

	}

	public GameObject CurrentSlice{
		get{
			return slices[_currentSliceIndex];
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
		for (int i =0; i<slices.Length; i++) {
			if (i!=startSlice){
				Physics2D.IgnoreLayerCollision (characterController.gameObject.layer,slices[_currentSliceIndex].layer,true);
			}
		}
	}

	public bool CanMove(Vector2 position,bool deep){
		Debug.Log ("Called can move " + CurrentSliceIndex + deep);
		int layerindex=_currentSliceIndex;
		if (deep && layerindex - 1 > -1) {
			layerindex -=1;
		}
		if (!deep && layerindex + 1 < slices.Length) {
			layerindex+=1;
		}


		if((_currentSliceIndex == 0 && deep) || (_currentSliceIndex == slices.Length-1 && !deep)){
			//Debug.Log ("NON MUOVERTI" + (_currentSlice == 0 && deep) + " " + (_currentSlice == slices.Length-1 && !deep));
			return false;
		}else{
			RaycastHit2D hit;
			if(deep){
				hit = Physics2D.Raycast(position, Vector2.zero, SLICE_DEPTH + 1, slices[layerindex].layer);
				Debug.Log ("Ho colpito in avanti");
				//Debug.DrawRay(position, Vector3.forward*(SLICE_DEPTH + 1), Color.blue);
			}else{
				hit = Physics2D.Raycast(position, -Vector2.zero, SLICE_DEPTH + 1, slices[layerindex].layer);
				Debug.Log("Ho colpto indietro");
				//Debug.DrawRay(position, Vector3.back*(SLICE_DEPTH + 1), Color.red);
			}
			if(hit.collider == null){
				return true;
			}else{
				Debug.Log ("Ho colpito " + hit.collider.name);
				return false;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void moved( bool deep){
		//TODO
		Debug.Log(characterController.gameObject.layer + " " + slices[_currentSliceIndex].layer + " " + _currentSliceIndex);

		Physics2D.IgnoreLayerCollision (characterController.gameObject.layer,slices[_currentSliceIndex].layer,true);
		CurrentSliceIndex = (deep? -1 :+1)+CurrentSliceIndex;
        Physics2D.IgnoreLayerCollision (characterController.gameObject.layer,slices[_currentSliceIndex].layer,false);

		Debug.Log(characterController.gameObject.layer + " " + slices[_currentSliceIndex].layer + " " + _currentSliceIndex);

	}
}
