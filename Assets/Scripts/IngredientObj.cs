using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObj : MonoBehaviour
{
    public SolidType solidType;
    private Shaker shaker;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject shakerOBJ = GameObject.Find("Shaker");
        shaker = shakerOBJ.GetComponent<Shaker>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Nozzle")
        {
            Debug.Log("Attached  " + collision.collider.name);
            shaker.addIngredient(new SolidIngredient(solidType), 1);
        }
    }
}