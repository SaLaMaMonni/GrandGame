using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text gameStatus;
    private GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (manager.hasWon)
        {
            gameStatus.text = "You've beaten the demo, congratulations! Press ESC to access pause menu";
        }

        if (manager.hasLost)
        {
            gameStatus.text = "You got overwhelmed with anxiety. You can always try again! Press ESC to access pause menu";
        }
    }
}
