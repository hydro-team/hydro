using UnityEngine;
using System.Collections;

public class Swipe : Gesture
{
	public const float PERCENT_TO_CANCEL = 0.10f;
	float maxLen;

	bool _canceled;
	/// <summary>
	/// gets if the swipe counts as canceled
	/// </summary>
	/// <value><c>true</c> if this swipe has been canceled; otherwise, <c>false</c>.</value>
	public bool Canceled{
		get{
			return _canceled;
		}

//		set{
//			_canceled = value;
//		}
	}

	float _lenght;


	/// <summary>
	/// Gets the lenght in px.
	/// </summary>
	/// <value>The lenght.</value>
	public float Lenght {
		get{ return _lenght;}
	}

	Vector2 _start;
	public Vector2 Start {
		get{ return _start;}
	} 
	Vector2 _end;

	public Vector2 End {

		get{ return _end;}
		set {
			_lenght = Vector2.Distance (_start, value);
			maxLen = _lenght>maxLen?_lenght:maxLen;
			_canceled = _lenght/maxLen < PERCENT_TO_CANCEL ;
			_end = value;
		}
	}

	public Swipe (Vector2 start)
	{
		this._start = start;
		_type = GestureType.SWIPE;
	}


}
