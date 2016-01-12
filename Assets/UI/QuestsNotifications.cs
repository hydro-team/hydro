using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Quests;

public class QuestsNotifications : MonoBehaviour {

    public Text notification;
    public QuestsEnvironment environment;

    Queue<string> messages = new Queue<string>();
    bool showingNotification = false;

    void Awake() {
        environment.OnQuestStarted += NotifyQuestStarted;
        environment.OnQuestSucceeded += NotifyQuestSucceeded;
        environment.OnQuestFailed += NotifyQuestFailed;
        environment.OnNewObjective += NotifyObjectiveStarted;
        environment.OnObjectiveSucceeded += NotifyObjectiveSucceeded;
        environment.OnObjectiveFailed += NotifyObjectiveFailed;
    }

    void Update() {
        if (showingNotification) { return; }
        if (messages.Count == 0) { return; }
        StartCoroutine(ShowNotification(messages.Dequeue()));
    }

    IEnumerator ShowNotification(string message) {
        showingNotification = true;
        notification.gameObject.SetActive(true);
        notification.text = message;
        var fadeTime = 2f;
        var color = notification.color;
        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeTime) {
            color.a = t;
            notification.color = color;
            yield return null;
        }
        color.a = 1f;
        notification.color = color;
        yield return new WaitForSeconds(5f);
        for (float t = 1f; t > 0f; t -= Time.deltaTime / fadeTime) {
            color.a = t;
            notification.color = color;
            yield return null;
        }
        color.a = 0f;
        notification.color = color;
        notification.gameObject.SetActive(false);
        showingNotification = false;
    }

    void NotifyQuestStarted(ActiveQuest started) {
        messages.Enqueue(FormatMessage("QUEST STARTED", started.quest.Name()));
    }

    void NotifyQuestSucceeded(ActiveQuest succeeded) {
        messages.Enqueue(FormatMessage("QUEST COMPLETED", succeeded.quest.Name()));
    }

    void NotifyQuestFailed(ActiveQuest failed) {
        messages.Enqueue(FormatMessage("QUEST FAILED", failed.quest.Name()));
    }

    void NotifyObjectiveStarted(ActiveQuest active, QuestObjective objective) {
        var optionalTag = objective.IsOptional() ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(active.quest.Name(), optionalTag + objective.Description()));
    }

    void NotifyObjectiveSucceeded(ActiveQuest active, QuestObjective objective) {
        var optionalTag = objective.IsOptional() ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(active.quest.Name(), "COMPLETED: " + optionalTag + objective.Description()));
    }

    void NotifyObjectiveFailed(ActiveQuest active, QuestObjective objective) {
        var optionalTag = objective.IsOptional() ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(active.quest.Name(), "FAILED: " + optionalTag + objective.Description()));
    }

    string FormatMessage(string title, string message) {
        return title + Environment.NewLine + Environment.NewLine + message;
    }
}
