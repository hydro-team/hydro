using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestGesture : MonoBehaviour,GestureEndObserver,GestureProgressObserver {
	public Text text;
	// Use this for initialization
	void Start () {
		GestureRecogniser.Recogniser.subscribeEnd (this);
		GestureRecogniser.Recogniser.subscribeProgress (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void notifyEnd(Gesture gesture){
		if (gesture.Type != Gesture.GestureType.TAP) {
			switch(gesture.Type){
			case Gesture.GestureType.SWIPE:
				text.text = gesture.Type.ToString () + " - canceled :" +((Swipe)gesture).Canceled;
				break;
			default:
				text.text = gesture.Type.ToString () + " - canceled :" +((Sprinch)gesture).Canceled;
				break;
			}
		} else {
			text.text = gesture.Type.ToString ();
		}
	}

	public void notifyProgress(Gesture gesture){
		switch (gesture.Type){
		case Gesture.GestureType.TAP:
			text.text = gesture.Type.ToString();
			break;
		case Gesture.GestureType.SWIPE:
			text.text = gesture.Type.ToString()+ " - length: " + ((Swipe)gesture).Lenght;
			break;
		default:
			text.text = gesture.Type.ToString()+ " - percentage: " + ((Sprinch)gesture).Percentage;
			break;
		}
	}
}
