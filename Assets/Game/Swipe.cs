using UnityEngine;
using System.Collections;

public class Swipe : Gesture
{

	float _lenght;

	public float Lenght {
		get{ return _lenght;}
	}

	Vector2 _start;
	public Vector2 Start {
		get{ return _start;}
	} 
	Vector2 _end;

	Vector2 End {

		get{ return _end;}
		set {
			_lenght = Vector2.Distance (Start, value);
			_end = value;
		}
	}

	public Swipe (Vector2 start)
	{
		this._start = start;
	}


}
