/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStress))]
public class PlayerStressCustomInspector : Editor
{
    //PlayerStress stress;
    public override void OnInspectorGUI()
    {
        PlayerStress stress = (PlayerStress)target;
        DrawDefaultInspector();

        if (!stress.hasCurve)
        {
            stress.gameObject.AddComponent<FollowAnimationCurve>();
        }
    }
}
*/