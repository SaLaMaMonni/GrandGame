using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteraction : Interactable
{
    GameObject player;

    private Animator animator;

    public float speed;
    private bool onTheMove = false;

    [SerializeField] Transform[] dogLocations;
    List<Vector3> locations;
    private Vector3 currentLocation;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;

        animator = GetComponent<Animator>();

        SetUpLocations();

        currentLocation = locations[0];
        transform.position = currentLocation;
    }

    public override void Update()
    {
        base.Update();

        // Checks whether the dog is supposed to move or not
        if (onTheMove)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    // Checks if the player is already interacting with the dog and if not, petting starts.
    public override void Interact()
    {
        if (!GameManager.Instance.isInteracting)
        {
            StartPetting();
            textMesh.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Causes the player to pet the dog, relieving their stress.
    // Won't last forever, dog will stop the petting moment and walk somewhere else.
    // Deactivates player's renderer so its sprite isn't visible during animation.
    void StartPetting()
    {
        GameManager.Instance.isInteracting = true;
        player.GetComponent<Renderer>().enabled = false;
        animator.SetTrigger("pet");
    }

    // Dog will walk away after petting is done.
    // Also reactivates player's renderer since the specific animation is done.
    public void StartMovingAway()
    {
        player.GetComponent<Renderer>().enabled = true;
        onTheMove = true;
    }

    // Finds a new random location for the dog and teleports it there.
    // After teleportation, allows the player to move again.
    void TeleportAway()
    {
        Vector3 randomLocation = RandomPosition();
        currentLocation = randomLocation;
        transform.position = currentLocation;

        GameManager.Instance.isInteracting = false;
        textMesh.GetComponent<MeshRenderer>().enabled = true;
    }

    // Returns a random location from a list.
    private Vector3 RandomPosition()
    {
        int rounds = 0;
        Vector3 randomPos = currentLocation;

        bool randomPosFound = false;

        while (rounds < 20)
        {
            randomPos = locations[Random.Range(0, locations.Count)];

            if (randomPos != currentLocation)
            {
                randomPosFound = true;
                return randomPos;
            }
            else
            {
                rounds++;
            }
        }

        // If it couldn't find a new location, warns about it.
        if (!randomPosFound)
        {
            Debug.Log("Couldn't find a random waypoint, check the code");
        }

        return randomPos;
    }

    // Acts accordingly when dog disappears from the camera view.
    public override void OnBecameInvisible()
    {
        base.OnBecameInvisible();

        if (onTheMove)
        {
            onTheMove = false;
            animator.SetTrigger("hidden");

            TeleportAway();
        }
    }

    // Sets up all the possible locations for dog when the game starts.
    void SetUpLocations()
    {
        locations = new List<Vector3>();

        foreach(Transform location in dogLocations)
        {
            locations.Add(location.position);
        }
    }
}
