using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton

    private static QuestManager instance = null;

    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Quest Manager Instance not found!");
            }

            return instance;
        }
    }

    #endregion

    private NPCInteraction[] quests;

    private int currentQuest = 1;       // Main quests start from 1. Quest with ID 0 are side quests

    int highestQuestNumber;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OrganizeQuests();
    }

    public void OrganizeQuests()
    {
        currentQuest = 1;

        NPCInteraction[] tempList = FindObjectsOfType<NPCInteraction>();
        quests = new NPCInteraction[tempList.Length + 1];

        foreach (NPCInteraction quest in tempList)
        {
            if (quest.questId > 0)
            {
                int id = quest.questId;
                //Debug.Log("Quest ID: " + id + " and quest list length: " + quests.Length + "and name: " + quest.name);

                quests[id] = quest;

                if (quest.questId > highestQuestNumber)
                {
                    highestQuestNumber = quest.questId;
                }
            }
        }

        OpenFirstQuest();
    }

    void OpenFirstQuest()
    {
        quests[currentQuest].isQuestOpen = true;
        //Debug.Log("Quests in total: " + highestQuestNumber);
    }

    public void OpenNextQuest()
    {
        currentQuest++;

        //Debug.Log("Current quest: " + currentQuest);

        if (currentQuest <= highestQuestNumber)
        {
            quests[currentQuest].isQuestOpen = true;
        }
        else
        {
            Debug.Log("That was the last quest. Congratulations!");
            GameManager.Instance.hasWon = true;
        }
    }
}
