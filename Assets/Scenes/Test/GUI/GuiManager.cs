using UnityEngine;
using Gestures;
using System;

public class GuiManager : MonoBehaviour {

    public GesturesDispatcher gestures;
    public GameObject mapPane;
    public GameObject questPane;
    public GameObject optionsPane;

    GameObject active;

    public void Map() {
        OpenPop(mapPane);
    }

    public void Quest() {
        OpenPop(questPane);
    }

    public void Options() {
        OpenPop(optionsPane);
    }

    void OpenPop(GameObject pushed) {
        if (pushed == active) {
            active.SetActive(false);
            gestures.gameObject.SetActive(true);
            active = null;
            return;
        }
        if (active != null) {
            active.SetActive(false);
            pushed.SetActive(true);
            active = pushed;
            return;
        }
        pushed.SetActive(true);
        gestures.gameObject.SetActive(false);
        active = pushed;
    }
}
