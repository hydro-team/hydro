using UnityEngine;
using UnityEngine.UI;
using Gestures;

public class TestGesture : MonoBehaviour{
	public Text GestureText;
	public Text debug;
    public GameObject gestures;
	// Use this for initialization
	void Start () {

		Debug.Log ("Test gesture starts");
        var dispatcher = gestures.GetComponent<GesturesDispatcher>();
        dispatcher.OnGestureStart += this.startGesture;
        dispatcher.OnGestureProgress += this.notifyProgress;
        dispatcher.OnGestureEnd += this.notifyEnd;
	}


	void startGesture(Gesture gesture){
		Debug.Log ("START Gesture - "+gesture.Type.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		debug.text = gestures.GetComponent<GesturesRecogniser>().CurrentState.ToString();
	}

	public void notifyEnd(Gesture gesture){
	
		Debug.Log (gesture!=null? "Gesture exists":"Gesture null");
		if (gesture.Type != GestureType.TAP) {
			switch(gesture.Type){
			case GestureType.SWIPE:
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
		case GestureType.TAP:
			GestureText.text = gesture.Type.ToString();
			break;
		case GestureType.SWIPE:
			GestureText.text = gesture.Type.ToString()+ " - length: " + ((Swipe)gesture).Lenght;
			break;
		default:
			GestureText.text = gesture.Type.ToString()+ " - percentage: " + ((Sprinch)gesture).Percentage;
			break;
		}
		Debug.Log ("Progress for " + gesture.Type + " gesture");
	}
}
