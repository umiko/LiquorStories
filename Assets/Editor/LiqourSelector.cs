using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PourDetector))]
public class LiqourSelector : UnityEditor.Editor
{
    private LiquidType liqour;
    private int liqourIndex = 0;
    private string[] liqourStrings;

    private void Awake()
    {
        int index = 0;
        liqourStrings = new string[Enum.GetNames(typeof(LiquidType)).Length];
        foreach (LiquidType liqour in (LiquidType[])Enum.GetValues(typeof(LiquidType)))
        {
            liqourStrings[index] = liqour.ToString();
            index++;
        }
    }

    public override void OnInspectorGUI()
    {
        var PourDetector = target as PourDetector;
        // Draw the default inspector
        DrawDefaultInspector();
        liqourIndex = (int)PourDetector.liqourType;
        liqourIndex = EditorGUILayout.Popup(liqourIndex, liqourStrings);

        if (PourDetector.liqourType != (LiquidType)liqourIndex)
        {
            // Update the selected choice in the underlying object
            LiquidType liquidType = (LiquidType)liqourIndex;
            PourDetector.liqourType = liquidType;
            PourDetector.liquidColor = LiquidColour.getLiquidColor(liquidType);
            //PourDetector.UpdateLiqourType((LiquidType)liqourIndex);
            Debug.Log("set " + (LiquidType)liqourIndex);
            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
    }
}