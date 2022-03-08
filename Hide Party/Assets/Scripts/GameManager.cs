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
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
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
}
