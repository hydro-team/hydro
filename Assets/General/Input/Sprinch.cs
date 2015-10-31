using UnityEngine;
using System.Collections;

//TODO figlio dello swipe?
public class Sprinch : Gesture
{
	const float PERCENTAGE_TO_CANCEL = 0.15f; 

//	float _time;
//
//	public float Time {
//		get {
//			return _time;
//		}
//	}

	float initialDistance;

	Vector2[] _start = new Vector2[2];
	/// <summary>
	/// Initial points of the gesture
	/// </summary>
	/// <value>The start.</value>
	public Vector2[] Start {
		get {
			return _start;
		}
	}

	Vector2[] _end = new Vector2[2];

	public Vector2[] End {
		get {
			return _end;
		}
		set {
			_end = value;
			_canceled = Vector2.Distance(value[0],value[1])<=PERCENTAGE_TO_CANCEL;

		}
	}

	bool _canceled;
	public bool Canceled{
		get{
			return _canceled;
		}
	}

	Vector2[] _currentPoints = new Vector2[2];

	public Vector2[] CurrentPoints {
		get {
			return _currentPoints;
		}
		set {
			_currentPoints = value;
			float currDist = Vector2.Distance (value [0], value [1]);
			switch (_type) {
			case GestureType.SPRINCH:

				if (currDist < initialDistance) {
					_type = GestureType.PINCH;
				} else {
					_type = GestureType.SPREAD;
				}

				break;

			case GestureType.PINCH:

				maxminDist = currDist<maxminDist? currDist : maxminDist; 
				_percentage = 1f-currDist/initialDistance;
				_percentage = _percentage<0f?0f:_percentage; 
				break;

			case GestureType.SPREAD:
				maxminDist = currDist>maxminDist? currDist :maxminDist;
				_percentage = currDist/maxminDist;
				break;
			}
		}
	
	}

	//FIXME servirà?
	float maxminDist;

	float _percentage = 0f;

	public float Percentage {
		get {
			return _percentage;
		}
	}

	public Sprinch (Vector2 start1, Vector2 start2){
		_start [0] = start1;
		_start [1] = start2;
		initialDistance = Vector2.Distance (start1, start2);
		_type = GestureType.SPRINCH;
	}
}
