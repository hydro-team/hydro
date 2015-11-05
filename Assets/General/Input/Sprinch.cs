using UnityEngine;
using System.Collections;

//TODO figlio dello swipe?
public class Sprinch : Gesture
{
	const float PERCENTAGE_TO_CANCEL = 0.15f;
	const float TOLERANCE = 15f;

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
	

	bool _canceled;
	public bool Canceled{
		get{
			return _canceled;
		}
	}

	Vector2[] _endPoints = new Vector2[2];

	public Vector2[] EndPoints {
		get {
			return _endPoints;
		}
		set {
			_endPoints = value;
			float currDist = Vector2.Distance (value [0], value [1]);
			switch (_type) {
			    case GestureType.SPRINCH:
				    if(Mathf.Abs(currDist-initialDistance) > TOLERANCE) {
                        _type = (currDist < initialDistance) ? GestureType.PINCH : GestureType.SPREAD;
                    }
                    updateOnPinch(currDist);
                    updateOnSpread(currDist);
                    break;

			    case GestureType.PINCH:
                    updateOnPinch(currDist); break;

			    case GestureType.SPREAD:
                    updateOnSpread(currDist); break;
			}
			_canceled = _percentage <=PERCENTAGE_TO_CANCEL;
		}
	
	}

    private void updateOnPinch(float currDist) {
        maxminDist = currDist < maxminDist ? currDist : maxminDist;
        _percentage = 1f - currDist / initialDistance;
        _percentage = _percentage < 0f ? 0f : _percentage;
    }

    private void updateOnSpread(float currDist) {
        maxminDist = currDist > maxminDist ? currDist : maxminDist;
        _percentage = (currDist - initialDistance) / (maxminDist - initialDistance);
        _percentage = _percentage > 0f ? _percentage : 0f;
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
