using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public bool fog;

    public bool hide;

    public GameObject[] rooms;
    public GameObject playeri;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach(GameObject room in rooms)
        {
            RoomController cont = room.GetComponent<RoomController>();
            cont.fogOfWar = fog;
            cont.hideOnLeave = hide;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        playeri.SetActive(true);
    }
}
