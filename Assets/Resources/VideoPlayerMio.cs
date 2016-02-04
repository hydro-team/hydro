using UnityEngine;

public class VideoPlayerMio : MonoBehaviour {

    public string nextScene = "ui";

    MovieTexture movie;

    void Awake() {
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        movie.loop = false;
    }

    void Start() {
        movie.Play();
    }

    void Update() {
        if (Input.anyKey) { movie.Stop(); }
        if (!movie.isPlaying) { Application.LoadLevel(nextScene); }
    }
}
