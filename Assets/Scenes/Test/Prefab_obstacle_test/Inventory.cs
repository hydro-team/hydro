using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public int type;

	// Use this for initialization
	void Start () {
		type = 0;
	}

	public bool pickUp(string tag){
		if(type == 0){
			type = typed(tag);
			Debug.Log (type);
			return true;
		}else{
			return false;
		}
	}

	private int typed(string tag){
		switch(tag){
		case "seaweed": return 1;
		default: return 0;
		}
	}

	public void freeInventory(){
		type = 0;
	}

	public string getInventoy(){
		switch(type){
		case 0: return null;
		case 1: return "seaweed";
		default: return null;
		}
		type = 0;
	}
}
