using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public Items item;
	public SpriteRenderer bouble;
	public SpriteRenderer itemsprite;
	Color activeboublecolor;
	Color coloritem;

	// Use this for initialization
	void Start () {
		activeboublecolor = bouble.color;
		item = Items.EMPTY;
		bouble.color = Color.clear;
		coloritem = Color.white;
	}

	void Update(){
		if(item != Items.EMPTY){
			bouble.color = Color.Lerp(bouble.color, activeboublecolor, Time.deltaTime*2);
			itemsprite.color = Color.Lerp(itemsprite.color, coloritem, Time.deltaTime*2);
		}else{
			bouble.color = Color.Lerp(bouble.color, Color.clear, Time.deltaTime*2);
			itemsprite.color = Color.Lerp(itemsprite.color, Color.clear, Time.deltaTime*2);
		}
		if(itemsprite.color == Color.clear){
			itemsprite.sprite = null;
			coloritem = Color.white;
		}
	}

	public bool pickUp(Items tag, Sprite sprite){
		if(item == Items.EMPTY){
			item = tag;
			itemsprite.sprite = sprite;
			return true;
		}else{
			return false;
		}
	}
	public bool pickUp(Items tag, Sprite sprite,Color col){
		if(item == Items.EMPTY){
			item = tag;
			itemsprite.sprite = sprite;
			itemsprite.color = col;
			coloritem = col;
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

public enum Items {SEAWEED, EMPTY, MUDSHIELD, ENERGY};