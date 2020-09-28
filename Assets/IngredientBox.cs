using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public SolidType solidTypes;
    public int spawnNum = 12;
    private SolidIngredient ingredient;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Instantiate(, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.)
    }
}