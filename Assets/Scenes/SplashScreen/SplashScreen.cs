using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public string nextScene;
    public float waitTime;
    public RawImage splashImage;

    void Start() {
        StartCoroutine(Run());
    }

    IEnumerator Run() {
        yield return new WaitForSeconds(waitTime);
        var color = splashImage.color;
        var alpha = color.a;
        while (alpha > 0) {
            alpha -= Time.deltaTime;
            if (alpha < 0) { alpha = 0; }
            splashImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }
}
