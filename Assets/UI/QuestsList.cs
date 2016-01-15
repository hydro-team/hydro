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
            foreach (var active in activeQuests) {
                builder.AppendLine("--------").AppendLine()
                       .Append(active.quest.Name()).AppendLine(":")
                       .AppendLine(active.quest.Description());
                foreach (var objective in active.CurrentObjectives()) {
                    builder.AppendLine();
                    if (objective.IsOptional()) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.Description());
                }
                foreach (var objective in active.SucceededObjectives().Reverse()) {
                    builder.AppendLine().Append("COMPLETED: ");
                    if (objective.IsOptional()) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.Description());
                }
            }
        } else {
            builder.AppendLine("No active quest...");
        }
        if (succeededQuests.Count() > 0) {
            builder.AppendLine().AppendLine("COMPLETED QUESTS").AppendLine();
            foreach (var completed in succeededQuests) {
                builder.AppendLine("--------").AppendLine()
                       .AppendLine(completed.quest.Name())
                       .AppendLine(completed.quest.Description());
                foreach (var objective in completed.SucceededObjectives()) {
                    builder.AppendLine();
                    if (objective.IsOptional()) { builder.Append("(OPTIONAL) "); }
                    builder.AppendLine(objective.Description());
                }
            }
        }
        text.text = builder.ToString();
    }

    void UpdateQuestsText(ActiveQuest quest) { UpdateQuestsText(); }
    void UpdateQuestsText(ActiveQuest quest, QuestObjective objective) { UpdateQuestsText(); }

    void AddNewQuest(ActiveQuest quest) {
        quests.Add(quest);
        UpdateQuestsText();
    }
}
