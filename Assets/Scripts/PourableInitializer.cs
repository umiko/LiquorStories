using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PourableInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 position, orientation;
    private PourDetector pd;
    void Start()
    {
        /*
        Get Drink Type
        Set Labels
        Set Liquid Color
        */

        pd = GetComponent<PourDetector>();
        LiquidType contents = pd.liqourType;
        TextMeshPro[] labels = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro textMeshPro in labels)
        {
            textMeshPro.text = contents.ToString("G");
        }
        Color liquidColor = LiquidColour.getLiquidColor(contents)*(1f/255f);
        liquidColor.a = 1;
        Renderer liquidRenderer = gameObject.GetComponentInChildren<Wobble>().gameObject.GetComponent<Renderer>();
        liquidRenderer.material.SetColor("_Tint", liquidColor);
        liquidRenderer.material.SetColor("_TopColor", liquidColor + new Color(0.1f,0.1f,0.1f, 1 ));
        liquidRenderer.material.SetColor("_FoamColor", liquidColor + new Color(0.1f,0.1f,0.1f, 1 ));
        var particleSystemMainModule = GetComponent<ParticleSystem>().main;
        particleSystemMainModule.startColor = liquidColor;
        position = transform.position;
        orientation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    public void Respawn()
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(orientation);
        gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
        pd.volume = 0;
    }
}
