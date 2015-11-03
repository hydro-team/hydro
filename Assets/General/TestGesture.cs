using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestGesture : MonoBehaviour,GestureEndObserver,GestureProgressObserver {
	public Text GestureText;
	public Text debug;
	// Use this for initialization
	void Start () {
		GestureRecogniser.Recogniser.subscribeEnd (new GestureRecogniser.ProcessGestureEvent(notifyEnd));
		GestureRecogniser.Recogniser.subscribeProgress (new GestureRecogniser.ProcessGestureEvent(notifyProgress));
	}
	
	// Update is called once per frame
	void Update () {
		debug.text = GestureRecogniser.Recogniser.CurrentState.ToString();
	}

	public void notifyEnd(Gesture gesture){
		Debug.Log (gesture!=null? "Gesture exists":"Gesture null");
		if (gesture.Type != Gesture.GestureType.TAP) {
			switch(gesture.Type){
			case Gesture.GestureType.SWIPE:
				GestureText.text = gesture.Type.ToString () + " - canceled :" +((Swipe)gesture).Canceled;
				break;
			default:
				Debug.Log(gesture.ToString());
				GestureText.text = gesture.Type.ToString () + " - canceled :" +((Sprinch)gesture).Canceled;
				break;
			}
		} else {
			GestureText.text = gesture.Type.ToString ();
		}
		Debug.Log ("End for " + gesture.Type + " gesture");
	}

	public void notifyProgress(Gesture gesture){
		switch (gesture.Type){
		case Gesture.GestureType.TAP:
			GestureText.text = gesture.Type.ToString();
			break;
		case Gesture.GestureType.SWIPE:
			GestureText.text = gesture.Type.ToString()+ " - length: " + ((Swipe)gesture).Lenght;
			break;
		default:
			GestureText.text = gesture.Type.ToString()+ " - percentage: " + ((Sprinch)gesture).Percentage;
			break;
		}
		Debug.Log ("Progress for " + gesture.Type + " gesture");
	}
}
