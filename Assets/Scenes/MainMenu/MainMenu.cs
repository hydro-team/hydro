using UnityEngine;
using System;
using Sound;

public class MainMenu : MonoBehaviour {

    SoundFacade sounds;
    Sound.Sound music;

    void Awake() {
        sounds = GetComponent<SoundFacade>();
        if (sounds == null) { throw new InvalidOperationException("Missing SoundFacade component"); }
    }

    void Start() {
        music = sounds.Play("/ambientali/background");
    }

    public void LoadTutorial() {
        music.Stop();
        Application.LoadLevel("Demomerge");
    }
}
