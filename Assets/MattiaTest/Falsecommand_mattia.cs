using UnityEngine;
using System.Collections;

public class Falsecommand_mattia : MonoBehaviour {


	public SliceMovement mov;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			mov.movementOnZ(true);
		}
		if(Input.GetKeyDown(KeyCode.L)){
			mov.movementOnZ(false);
		}
	}
}
