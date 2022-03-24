using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text gameStatus;
    public GameObject hud;
    private GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.hasLost || GameManager.Instance.hasWon)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        hud.SetActive(true);

        if (manager.hasWon)
        {
            gameStatus.text = "You've beaten the demo, congratulations! Press ESC to quit";
        }

        if (manager.hasLost)
        {
            gameStatus.text = "You got overwhelmed with anxiety and because of that, you're shutting down. " +
                "You can always try again, just be more careful. Press ESC to exit";
        }
    }
}
