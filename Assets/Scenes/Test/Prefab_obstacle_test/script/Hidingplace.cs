using UnityEngine;
using System.Collections;

public class Hidingplace : MonoBehaviour
{
	private BoxCollider2D coll;
	private Vector2 rightdisplacemetn;
	private Vector2 leftdisplacemetn;

	// Use this for initialization
	void Start ()
	{
		coll = GetComponent<BoxCollider2D>();
		coll.enabled = false;
		rightdisplacemetn = new Vector2(-0.31f,0f);
		leftdisplacemetn = new Vector2(0.11f,0f);
	}
	
	public void active(bool right_left){
		coll.enabled = true;
		if(right_left){
			coll.offset = rightdisplacemetn;
		}else{
			coll.offset = leftdisplacemetn;
		}

	}
	public void deActive(){
		coll.enabled = false;
	}
}

