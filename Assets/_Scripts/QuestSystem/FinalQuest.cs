using UnityEngine;
public class FinalQuest : Quest
{
    [SerializeField]
    public Loader.Scene LoadScene;
    protected override void Awake()
    {
        base.Awake();
        QuestCompleted.AddListener(()=>Loader.Load(LoadScene));
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
