using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    bool IsSequenceMode;
    [SerializeField]
    public List<Quest> quests = new List<Quest>();
    private int questNum=0;
    public UnityEvent ALLQUESTCOMPLETED;
    public Quest CurrentQuest;
    private void Awake()
    {
        foreach (Quest quest in quests)
        {
            quest.QuestCompleted.AddListener(OnQuestCompleted);
        }
        CurrentQuest = quests[0];
    }
    public void AddQuest(Quest newQuest)
    {
        quests.Add(newQuest);
    }
    public void OnQuestCompleted()
    {
        if(IsSequenceMode)
        {
            NextQuest();
        }
        if (quests.All(g => g.IsReached()))
            ALLQUESTCOMPLETED.Invoke();
    }

    private void NextQuest()
    {
        if (questNum != quests.Count)
        {
            CurrentQuest = quests[++questNum];
        }
        
    }
    
}

//public class QuestGiver : MonoBehaviour
//{
//    public QuestManager questManager;

//    void Start()
//    {
//        // Добавление тестового квеста при запуске
//        Quest testQuest = new Quest("Find the Treasure", "Find and retrieve the hidden treasure.");
//        questManager.AddQuest(testQuest);
//    }

//    void Update()
//    {
//        // Завершение тестового квеста по нажатию клавиши
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            questManager.CompleteQuest("Find the Treasure");
//        }

//        // Проверка состояния тестового квеста по нажатию клавиши
//        if (Input.GetKeyDown(KeyCode.V))
//        {
//            bool isCompleted = questManager.IsQuestCompleted("Find the Treasure");
//            Debug.Log("Is 'Find the Treasure' quest completed? " + isCompleted);
//        }
//    }
//}
