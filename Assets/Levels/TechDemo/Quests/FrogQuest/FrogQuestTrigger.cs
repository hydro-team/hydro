using UnityEngine;
using Quests;

public class FrogQuestTrigger : MonoBehaviour {

    public QuestsEnvironment environment;
    public Inventory hydroInventory;

    FrogQuest.Context context;

    void OnTriggerEnter2D(Collider2D other) {
        if (context != null) { return; }
        if (other.gameObject.layer != LayerMask.NameToLayer("Hydro")) { return; }
        environment.gameObject.AddComponent<ActiveQuest>().Begin(new FrogQuest());
        context = environment.GetComponent<FrogQuest.Context>();
    }

    void Update() {
        if (context == null) { return; }
        if (!context.collectedSeaweed && hydroInventory.getInventoy() == Items.SEAWEED) {
            context.collectedSeaweed = true;
        }
        if (!context.feedTadpole && context.collectedSeaweed && hydroInventory.getInventoy() == Items.EMPTY) {
            context.feedTadpole = true;
        }
    }
}
