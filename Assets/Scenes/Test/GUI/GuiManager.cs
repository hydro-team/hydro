using UnityEngine;
using Gestures;
using System;

public class GuiManager : MonoBehaviour {

    public GesturesDispatcher gestures;
    public QuestsNotifications quests;
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
            EnableInteractions();
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
        DisableInteractions();
        active = pushed;
    }

    void EnableInteractions() {
        gestures.gameObject.SetActive(true);
        quests.Show();
    }

    void DisableInteractions() {
        gestures.gameObject.SetActive(false);
        quests.Hide();
    }
}
