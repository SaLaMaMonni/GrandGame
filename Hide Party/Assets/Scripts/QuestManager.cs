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

    void OrganizeQuests()
    {
        NPCInteraction[] tempList = FindObjectsOfType<NPCInteraction>();
        quests = new NPCInteraction[tempList.Length + 1];

        foreach (NPCInteraction quest in tempList)
        {
            if (quest.questId > 0)
            {
                int id = quest.questId;
                //Debug.Log("Quest ID: " + id + " and quest list length: " + quests.Length);

                quests[id] = quest;
            }
        }

        OpenFirstQuest();
    }

    void OpenFirstQuest()
    {
        quests[currentQuest].isQuestOpen = true;
    }

    public void OpenNextQuest()
    {
        currentQuest++;

        Debug.Log(currentQuest + quests.Length);

        if (currentQuest < quests.Length)
        {
            quests[currentQuest].isQuestOpen = true;
        }
        else
        {
            Debug.Log("That was the last quest. Congratulations!");
        }
    }
}
