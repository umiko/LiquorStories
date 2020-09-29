using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject[] ingredients;
    public int spawnNum = 2;
    private Queue<SolidType> queue = new Queue<SolidType>();

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(SpawnNewIngredient));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SolidIngredient"))
        {
            queue.Enqueue(other.GetComponent<IngredientObj>().solidType);
            StartCoroutine(nameof(RespawnIngredient));
            Destroy(other.gameObject, 180f);
        }
    }
    
    private IEnumerator RespawnIngredient()
    {
        SolidType type = queue.Dequeue();
        foreach (GameObject ingredient in ingredients)
        {
            
            if (ingredient.GetComponent<IngredientObj>().solidType == type)
            {
                yield return new WaitForSeconds(10f);
                Instantiate(ingredient, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
            }
        }
    }

    private IEnumerator SpawnNewIngredient()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            foreach (GameObject ingredient in ingredients)
            {
                Instantiate(ingredient, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
