using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.Rendering.UI;
using System.Linq;
public class Quest : MonoBehaviour
{
    public string Title;
    public string Description;
    private bool isReached;
    [HideInInspector]
    public UnityEvent QuestCompleted;
    [SerializeField]
    public List<Goal> Goals;
    private void Awake()
    {
        QuestCompleted.AddListener(() => isReached = true);
        foreach (var goal in Goals)
        {
            goal.GoalCompleted.AddListener(OnGoalCompleted);
        }    
    }
    public bool IsReached()
    {
        return isReached;
    }
    private void OnGoalCompleted()
    {

        Debug.Log("QUes's Goal completed");
        foreach (var goal in Goals)
        {
            Debug.Log("Goal" + goal.Title);
            if (!goal.IsReached)
            {
                Debug.Log("not conpleted");


                return;
            }
            Debug.Log("completed");

        }
        QuestCompleted.Invoke();
        Debug.Log("QUestComlpeted");
        isReached = true;
    }
}

//public class QuestGiver : MonoBehaviour
//{
//    public QuestManager questManager;

//    void Start()
//    {
//        // ƒобавление тестового квеста при запуске
//        Quest testQuest = new Quest("Find the Treasure", "Find and retrieve the hidden treasure.");
//        questManager.AddQuest(testQuest);
//    }

//    void Update()
//    {
//        // «авершение тестового квеста по нажатию клавиши
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            questManager.CompleteQuest("Find the Treasure");
//        }

//        // ѕроверка состо€ни€ тестового квеста по нажатию клавиши
//        if (Input.GetKeyDown(KeyCode.V))
//        {
//            bool isCompleted = questManager.IsQuestCompleted("Find the Treasure");
//            Debug.Log("Is 'Find the Treasure' quest completed? " + isCompleted);
//        }
//    }
//}
