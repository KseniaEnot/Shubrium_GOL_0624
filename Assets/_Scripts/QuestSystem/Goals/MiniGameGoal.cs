using Assets._Scripts.Minigames.TowerGame;
using System;
using UnityEngine;

public class MiniGameGoal : Goal
{
    [SerializeField]
    MiniGame game;
    public void Awake()
    {
        game.GameProgressChanged.AddListener(ProgressChanged);
    }
}
//public class QuestManager : MonoBehaviour
//{
//    public List<Quest> quests = new List<Quest>();

//    // Добавление нового квеста
//    public void AddQuest(Quest newQuest)
//    {
//        quests.Add(newQuest);
//    }

//    // Завершение квеста
//    public void CompleteQuest(string questName)
//    {
//        foreach (Quest quest in quests)
//        {
//            if (quest.questName == questName)
//            {
//                quest.isCompleted = true;
//                Debug.Log("Quest Completed: " + questName);
//                return;
//            }
//        }
//        Debug.LogWarning("Quest not found: " + questName);
//    }

//    // Проверка состояния квеста
//    public bool IsQuestCompleted(string questName)
//    {
//        foreach (Quest quest in quests)
//        {
//            if (quest.questName == questName)
//            {
//                return quest.isCompleted;
//            }
//        }
//        Debug.LogWarning("Quest not found: " + questName);
//        return false;
//    }
//}

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
