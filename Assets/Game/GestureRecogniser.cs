using UnityEngine;
using System.Collections;

public class GestureRecogniser : MonoBehaviour
{

	GestureState state = GestureState.NEUTRAL;
	Gesture currentGesture;

	const float tapMaxDragDistance = 10f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state) {
		default:
			beginRecognition ();
			break;
		}
	
	}

	enum GestureState
	{
		SWIPE_BEGIN,
		SWIPE_END,
		PINSPR_START,
		SPREAD,
		PINCH,
		SPREAD_END,
		PINCH_END,
		PINSPR_NEUTRAL,
		TAP_BEGIN,
		NEUTRAL,
	}

	void beginRecognition ()
	{
		if (Input.touches.Length == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				currentGesture = new Tap (Input.touches [0].position);
				state = GestureState.TAP_BEGIN;
			}
		}
		if (Input.touches.Length == 2) {
			//TODO PINCH/Spread
		}
	}
}

