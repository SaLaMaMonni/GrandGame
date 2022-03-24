using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Game Manager Instance not found!");
            }

            return instance;
        }
    }

    #endregion

    public bool isInteracting = false;

    public NPCInteraction mattiObject;

    public GameObject dog;

    public bool hasWon = false;
    public bool hasLost = false;

    public GameObject blackout;

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

    private void Update()
    {
        /*
        // For quick testing
        if (Input.GetKeyDown(KeyCode.G))
        {
            hasWon = true;
        }
        */
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }

    public void HideMatti()
    {
        mattiObject.GiveItemRemotely();

        Vector2 hiding = new Vector3(0, -30, 0);
        mattiObject.transform.position = hiding;

        RevealDog();
    }

    // Dog gives out one of the quest items.
    // This stops the player from finding the dog too soon.
    public void RevealDog()
    {
        dog.SetActive(true);
    }

    public void NewGameSetUp()
    {
        mattiObject = GameObject.Find("Matti").GetComponent<NPCInteraction>();
        dog = GameObject.Find("Dog");
        dog.SetActive(false);
    }

    public void GameOver()
    {
        hasLost = true;
        blackout.SetActive(true);
        Time.timeScale = 0f;
    }
}
