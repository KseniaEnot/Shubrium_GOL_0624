using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class QuestsUI : MonoBehaviour
{
    [SerializeField]
    private QuestManager QuestManager;
    private UIDocument document;
    private VisualElement root;
    private GroupBox QuestsList;
    private List<Label> QuestsLabels = new();
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        QuestsList = root.Q("QuestsList") as GroupBox;
        foreach (VisualElement lable in QuestsList.Children())
        {
            QuestsLabels.Add(lable as Label);
        }
        int i = 0;
        foreach(Quest quest in QuestManager.quests)
        {
            int j = i;
            QuestsLabels[j].text = quest.Title + ": 0/1";
            quest.QuestCompleted.AddListener(() => OnQuestComleted(j));
            quest.Goals[0].GoalProgressChanged.AddListener((a,b)=>GoalChanged(a,b,j)); ;
            i++;
        }
    }
    public void SetFinalQuestInfo()
    {
        QuestsLabels[0].text = QuestManager.quests[0].Title + ": 0/1";
        QuestsLabels[1].text = "";
        QuestsLabels[2].text = "";
        QuestsLabels[3].text = "";
        foreach(var VisualElement in CompleteLines)
        {
            VisualElement.style.height = 0;
        }
    }
    public void GoalChanged(int newscore,int max,int i)
    {
        int colonIndex = QuestsLabels[i].text.IndexOf(':');
        string scorestr = GetScoreStr(QuestsLabels[i].text);
        scorestr = ": " + newscore + "/" + max;
        QuestsLabels[i].text = QuestsLabels[i].text.Substring(0, colonIndex) + scorestr;
        Debug.Log(scorestr);
        Debug.Log("GoalProgressTEXTCHANGED"+newscore+" "+ max+ " " +i);
    }
    private List<VisualElement> CompleteLines = new List<VisualElement>();
    public void OnQuestComleted(int i)
    {
        VisualElement line = new VisualElement();
        line.style.position = Position.Absolute;
        line.style.left = 0;
        line.style.right = 0;
        line.style.top = 17; // положение по середине текста
        line.style.height = 1;
        line.style.backgroundColor = Color.black;
        QuestsLabels[i].Add(line);
        CompleteLines.Add(line);
        int colonIndex = QuestsLabels[i].text.IndexOf(':');
        QuestsLabels[i].text = QuestsLabels[i].text.Substring(0, colonIndex) + ": 1/1";
        Debug.Log("QuestCompleted");
    }
    public string GetScoreStr(string a)
    {
        int colonIndex = a.IndexOf(':');
        string scorestr;
        if (colonIndex == -1 || colonIndex == a.Length - 1)
        {
            Debug.Log("Not found ':'");
            return null;
        }
        scorestr = a.Substring(colonIndex + 1);
        return scorestr;
    }
}
