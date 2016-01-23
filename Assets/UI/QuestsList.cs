using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Quests;

public class QuestsList : MonoBehaviour {

    public QuestsEnvironment environment;
    public Text text;

    IList<ActiveQuest> quests = new List<ActiveQuest>();

    void Awake() {
        if (text == null) { throw new InvalidOperationException("QuestsList must be attached to a Text UI component"); }
        environment.OnQuestStarted += AddNewQuest;
        environment.OnQuestSucceeded += UpdateQuestsText;
        environment.OnQuestFailed += UpdateQuestsText;
        environment.OnNewObjective += UpdateQuestsText;
        environment.OnObjectiveSucceeded += UpdateQuestsText;
        environment.OnObjectiveFailed += UpdateQuestsText;
    }

    void UpdateQuestsText() {
        var activeQuests = quests.Where(quest => quest.Status == ProgressStatus.ONGOING);
        var succeededQuests = quests.Where(quest => quest.Status == ProgressStatus.SUCCEEDED);
        var failedQuests = quests.Where(quest => quest.Status == ProgressStatus.FAILED);
        var builder = new StringBuilder().AppendLine("QUESTS").AppendLine();
        if (activeQuests.Count() > 0) {
            foreach (var quest in activeQuests) {
                builder.AppendLine("--------").AppendLine()
                       .Append(quest.Name).AppendLine(":")
                       .AppendLine(quest.Description);
                foreach (var objective in quest.CurrentObjectives()) {
                    builder.AppendLine();
                    if (objective.isOptional) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.description());
                }
                foreach (var objective in quest.SucceededObjectives().Reverse()) {
                    builder.AppendLine().Append("COMPLETED: ");
                    if (objective.isOptional) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.description());
                }
            }
        } else {
            builder.AppendLine("No active quest...");
        }
        if (succeededQuests.Count() > 0) {
            builder.AppendLine().AppendLine("COMPLETED QUESTS").AppendLine();
            foreach (var quest in succeededQuests) {
                builder.AppendLine("--------").AppendLine()
                       .AppendLine(quest.Name)
                       .AppendLine(quest.Description);
                foreach (var objective in quest.SucceededObjectives()) {
                    builder.AppendLine();
                    if (objective.isOptional) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.description());
                }
            }
        }
        text.text = builder.ToString();
    }

    void UpdateQuestsText(ActiveQuest quest) { UpdateQuestsText(); }

    void UpdateQuestsText(ActiveQuest quest, ActiveQuest.Objective objective) { UpdateQuestsText(); }

    void AddNewQuest(ActiveQuest quest) {
        quests.Add(quest);
        UpdateQuestsText();
    }
}
