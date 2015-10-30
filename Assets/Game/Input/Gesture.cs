using UnityEngine;
using System.Collections;

public class Gesture
{
	protected GestureType _type;

	public GestureType Type{
		get{
			return _type;
		}
	}

	public enum GestureType{
		TAP,
		SPREAD,
		PINCH,
		SWIPE
	}

}
