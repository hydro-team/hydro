using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Sounds;

public class MainMenu : MonoBehaviour {

    public String firstScene;

    SoundFacade sounds;
    Sound music;

    void Awake() {
        sounds = GetComponent<SoundFacade>();
        if (sounds == null) { throw new InvalidOperationException("Missing SoundFacade component"); }
    }

    void Start() {
        music = sounds.Play("/ambientali/background");
    }

    public void LoadFirstScene() {
        music.Stop();
        SceneManager.LoadScene(firstScene);
    }
}
