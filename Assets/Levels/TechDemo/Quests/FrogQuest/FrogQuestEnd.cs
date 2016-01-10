using UnityEngine;
using Quests;

public class FrogQuestEnd : MonoBehaviour {

    public QuestsEnvironment environment;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Hydro")) { return; }
        var context = environment.GetComponent<FrogQuest.Context>();
        context.jumpedOver = true;
        gameObject.SetActive(false);
    }
}
