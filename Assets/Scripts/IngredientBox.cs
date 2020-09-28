using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject[] ingredients;
    public int spawnNum = 2;
    private Queue<GameObject> queue = new Queue<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(SpawnNewIngredient));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SolidIngredient"))
        {
            StartCoroutine(nameof(RespawnIngredient));
            Destroy(other.gameObject, 180f);
        }
    }
    
    private IEnumerator RespawnIngredient()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            foreach (GameObject ingredient in queue)
            {
                yield return new WaitForSeconds(3f);
                Instantiate(ingredient, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                yield return new WaitForSeconds(.3f);
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
