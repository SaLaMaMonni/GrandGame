using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CurveElement : EditorWindow
{

    PlayerStress stressScript;
    private void OnEnable()
    {

        if(stressScript == null)
        {
            stressScript = FindObjectOfType<PlayerStress>();

            if(!stressScript.hasCurve)
            {
                AddCurveComponent();
                stressScript.hasCurve = true;
            }
        }
    }


    private void AddCurveComponent()
    {
        Debug.Log("Added curve!");
    }
}
