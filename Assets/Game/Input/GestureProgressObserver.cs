using UnityEngine;
using System.Collections;

public interface GestureProgressObserver {

	void notify(Gesture gesture);
}
