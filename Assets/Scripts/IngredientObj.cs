using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObj : MonoBehaviour
{
    public SolidType solidType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Nozzle")
        {
            collision.collider.transform.parent.GetComponent<Shaker>().addIngredient(new SolidIngredient(solidType), 1);
            Destroy(this.gameObject);
        }
    }
}