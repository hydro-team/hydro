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
    bool hidden;

    public void Show() {
        notification.gameObject.SetActive(true);
        hidden = false;
    }
    public void Hide() {
        notification.gameObject.SetActive(false);
        hidden = true;
    }

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
        if (!hidden) { notification.gameObject.SetActive(true); }
        notification.text = message;
        var fadeTime = 0.5f;
        var color = notification.color;
        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeTime) {
            color.a = t;
            notification.color = color;
            yield return null;
        }
        color.a = 1f;
        notification.color = color;
        yield return new WaitForSeconds(2f);
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

    void NotifyQuestStarted(ActiveQuest quest) {
        messages.Enqueue(FormatMessage("QUEST STARTED", quest.Name));
    }

    void NotifyQuestSucceeded(ActiveQuest quest) {
        messages.Enqueue(FormatMessage("QUEST COMPLETED", quest.Name));
    }

    void NotifyQuestFailed(ActiveQuest quest) {
        messages.Enqueue(FormatMessage("QUEST FAILED", quest.Name));
    }

    void NotifyObjectiveStarted(ActiveQuest quest, ActiveQuest.Objective objective) {
        var optionalTag = objective.isOptional ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(quest.Name, optionalTag + objective.Description));
    }

    void NotifyObjectiveSucceeded(ActiveQuest quest, ActiveQuest.Objective objective) {
        var optionalTag = objective.isOptional ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(quest.Name, "COMPLETED" + Environment.NewLine + optionalTag + objective.Description));
    }

    void NotifyObjectiveFailed(ActiveQuest quest, ActiveQuest.Objective objective) {
        var optionalTag = objective.isOptional ? "(OPTIONAL) " : "";
        messages.Enqueue(FormatMessage(quest.Name, "FAILED" + Environment.NewLine + optionalTag + objective.Description));
    }

    string FormatMessage(string title, string message) {
        return title + Environment.NewLine + Environment.NewLine + message;
    }
}
