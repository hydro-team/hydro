using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    public GameObject buttonsPane;
    public GameObject creditsPane;

    public void ShowCredits() {
        buttonsPane.SetActive(false);
        creditsPane.SetActive(true);
    }

    public void HideCredits() {
        Debug.Log("hidden");
        creditsPane.SetActive(false);
        buttonsPane.SetActive(true);
    }
}
