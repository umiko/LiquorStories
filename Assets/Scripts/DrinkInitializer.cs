using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrinkInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    void Start()
    {
        /*
        Get Drink Type
        Set Labels
        Set Liquid Color
        */

        PourDetector pd = GetComponent<PourDetector>();
        LiquidType contents = pd.liqourType;
        TextMeshPro[] labels = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro textMeshPro in labels)
        {
            textMeshPro.text = contents.ToString("G");
        }
        Color liquidColor = LiquidColour.getLiquidColor(contents);
        Renderer liquidRenderer = gameObject.GetComponentInChildren<Wobble>().gameObject.GetComponent<Renderer>();
        liquidRenderer.material.SetColor("_Tint", liquidColor);
        liquidRenderer.material.SetColor("_TopColor", liquidColor + new Color(0.1f,0.1f,0.1f, 1 ));
        liquidRenderer.material.SetColor("_FoamColor", liquidColor+ new Color(0.1f,0.1f,0.1f, 1 ));
        var particleSystemMainModule = GetComponent<ParticleSystem>().main;
        particleSystemMainModule.startColor = liquidColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
