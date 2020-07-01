﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierExample))]
public class DrawBezierExample : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierExample be = target as BezierExample;
        be.startPoint = Handles.PositionHandle(be.startPoint, Quaternion.identity);
        be.endPoint = Handles.PositionHandle(be.endPoint, Quaternion.identity);
        be.startTangent = Handles.PositionHandle(be.startTangent, Quaternion.identity);
        be.endTangent = Handles.PositionHandle(be.endTangent, Quaternion.identity);

        Handles.DrawBezier(be.startPoint, be.endPoint, be.startTangent, be.endTangent, Color.red, be.bezierTexture, 2f);
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        //SceneView.onSceneGUIDelegate += OnSceneViewGUI;
        SceneView.duringSceneGui += OnSceneViewGUI;
        //texture = BezierExample.bezierTexture;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }
}