using UnityEngine;
using System.Collections;
/// <summary>
/// Gesture observer.
/// Inherit from this class to be notified about the end of a gesture
/// </summary>
public interface GestureEndObserver {

	void notify(Gesture gesture);
}
