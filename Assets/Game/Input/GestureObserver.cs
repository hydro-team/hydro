using UnityEngine;
using System.Collections;
/// <summary>
/// Gesture observer.
/// Inherit from this class to be notified about changes in the gesture
/// </summary>
public interface GestureObserver {

	void notify(Gesture gesture);

}
