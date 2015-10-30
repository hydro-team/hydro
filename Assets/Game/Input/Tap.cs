using UnityEngine;
using System.Collections;

public class Tap : Gesture
{


	Vector2 _position;

	public Vector2 Position {
		get {
			return _position;
		}
	}

	 private float _time;

	public float BeginTime{
		get{
			return _time;
		}
	}

	public Tap (Vector2 position , float time)
	{
		_position = position;
		_time = time;
		_type = GestureType.TAP;

	}

	public float getDuration(){
		return Time.time - _time;
	}

}
