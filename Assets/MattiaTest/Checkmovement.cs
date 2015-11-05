using UnityEngine;
using System.Collections;

public class Checkmovement : MonoBehaviour {

	public int dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//metodo per il controllo che sia possibile cambiare slice in questo punto
	public bool freePassage(){
		//Debug.Log ("Called");
		RaycastHit2D hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y), dir*Vector2.zero);
		if(hit.collider != null){
			//Debug.Log(hit.collider.name);
			Debug.DrawRay(transform.position, dir*Vector3.forward * 10, Color.magenta);
			return false;
		}
		return true;
	}
}
