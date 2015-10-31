using UnityEngine;
using System.Collections;

public interface GestureProgressObserver {

	void notifyProgress(Gesture gesture);
}
