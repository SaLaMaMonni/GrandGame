using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniManager : MonoBehaviour
{
    void Start()
    {
        Inventory.Instance.RestartInventory();
        GameManager.Instance.NewGameSetUp();
        QuestManager.Instance.OrganizeQuests();
    }
}
