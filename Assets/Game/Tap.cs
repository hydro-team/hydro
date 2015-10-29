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

	public Tap (Vector2 position)
	{
		_position = position;
	}

}
