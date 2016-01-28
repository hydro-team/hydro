using UnityEngine;
using System;
using Sounds;

public class MainMenu : MonoBehaviour {

    public String firstScene;
	public ScreenFader fade;
	public GameObject buttons;

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
		buttons.SetActive(false);
		fade.halfShadetoBlack(firstScene);
       // Application.LoadLevel(firstScene);
    }
}
