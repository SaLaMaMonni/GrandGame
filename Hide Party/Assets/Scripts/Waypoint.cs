using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    public int room;
    public int number;
    public Waypoint[] connections;

    private Vector3 namePrintPosOffset = new Vector3(-0.1f, 0.8f,0f);


    // Set values automatically. Too lazy to do any real editor/gui work.
    private void OnValidate()
    {
        string[] splitName = name.Split('_');

        room = int.Parse(splitName[0]);
        number = int.Parse(splitName[1]);
    }


    // Show connected waypoints.
    private void OnDrawGizmosSelected()
    {
        //Handles.Label(transform.position + namePrintPosOffset * -1f, name+" selected");

        foreach (Waypoint wp in connections)        {
            Gizmos.DrawLine(transform.position, wp.transform.position);
        }
    }

    // Show name on waypoint. Regular icon was way too small and terrible.
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + namePrintPosOffset, name);
    }


}
