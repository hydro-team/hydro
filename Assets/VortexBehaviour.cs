using UnityEngine;
using System.Collections;

public class VortexBehaviour : MonoBehaviour {
	
	public WorldManager wm;
    public float rotationSpeed = -500f;
	SpriteRenderer render;
	int slice;


	void Start () {
		slice = 0;
		render = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        var rotation = transform.rotation.eulerAngles;
        rotation.z += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
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
