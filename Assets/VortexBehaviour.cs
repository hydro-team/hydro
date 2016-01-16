using UnityEngine;
using System.Collections;

public class VortexBehaviour : MonoBehaviour {
	
	public WorldManager wm;
	SpriteRenderer render;
	int slice;


	void Start () {
		slice = 0;
		render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerExit2D(Collider2D other) {
		wm.exitVortex();
	}
	public void OnTriggerStay2D(Collider2D other) {
		int curslice = wm.CurrentSliceIndex;
		if(curslice == 0){
			wm.possiblePinch();
		}else{
			wm.possibleSpread();
		}
		if(slice != curslice){
			transform.position = new Vector3(transform.position.x, transform.position.y, wm.CurrentSliceZ);
			render.sortingLayerName = "Slice"+curslice;
			slice = curslice;
		}
	}
}
