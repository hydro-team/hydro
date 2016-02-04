using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Quests;

public class QuestsNotifications : MonoBehaviour {

    public Text notification;
    public QuestsEnvironment environment;
    public float messageDuration = 4f;

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
        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeTime) {
            setTextAlpha(t);
            yield return null;
        }
        setTextAlpha(1f);
        yield return new WaitForSeconds(messageDuration);
        for (float t = 1f; t > 0f; t -= Time.deltaTime / fadeTime) {
            setTextAlpha(t);
            yield return null;
        }
        setTextAlpha(0f);
        notification.gameObject.SetActive(false);
        showingNotification = false;
    }

    void setTextAlpha(float alpha) {
        var color = notification.color;
        color.a = alpha;
        notification.color = color;
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
