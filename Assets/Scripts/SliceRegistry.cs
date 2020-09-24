using System.Collections.Generic;
using UnityEngine;

public class SliceRegistry
{
    private readonly List<Transform> slicerTransforms;
    private static SliceRegistry _instance;

    private static SliceRegistry Instance => _instance ?? (_instance = new SliceRegistry());

    private SliceRegistry()
    {
        slicerTransforms = new List<Transform>();
    }

    public static void Register(Transform t)
    {
        Debug.Log("Registering Slicer");
        Instance.slicerTransforms.Add(t);
    }
    
    public static bool IsSlicer(Transform t)
    {
        return Instance.slicerTransforms.Contains(t);
    }
}
