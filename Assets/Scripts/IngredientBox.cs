using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public SolidType solidType;
    public GameObject[] ingredients;
    private GameObject objToSpawn;
    public int spawnNum = 2;

    // Start is called before the first frame update
    private void Start()
    {
        objToSpawn = ingredients[(int)solidType];
        StartCoroutine("spawnNewIngredient");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SolidIngredient")
        {
            spawnNum = 1;
            StartCoroutine("spawnNewIngredient");
            Destroy(other.gameObject, 20f);
        }
    }

    private IEnumerator spawnNewIngredient()
    {
        if (objToSpawn)
        {
            for (int i = 0; i < spawnNum; i++)
            {
                Debug.Log(transform.position);
                Instantiate(objToSpawn, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            Debug.LogError("IngredientBox could not find Ingredient");
            yield return null;
        }
    }
}