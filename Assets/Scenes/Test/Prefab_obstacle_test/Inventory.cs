using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public Items item;

	// Use this for initialization
	void Start () {
		item = Items.EMPTY;
	}

	public bool pickUp(Items tag){
		if(item == Items.EMPTY){
			item = tag;
			return true;
		}else{
			return false;
		}
	}

	public void freeInventory(){
		item = Items.EMPTY;
	}

	public Items getInventoy(){
		return (item);
	}
}

public enum Items {SEAWEED, EMPTY, MUDSHIELD};