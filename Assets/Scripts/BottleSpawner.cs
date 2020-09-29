using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    private PourDetector[] bottles;
    Dictionary<LiquidType, Transform> spawnPoints= new Dictionary<LiquidType, Transform>();
    // Start is called before the first frame update
    void Start()
    {
        bottles = GetComponentsInChildren<PourDetector>();
        StartCoroutine(nameof(SetSpawnPoints));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetSpawnPoints()
    {
        yield return new WaitForSeconds(2f);
        foreach (PourDetector pourDetector in bottles)
        {
            if(!spawnPoints.ContainsKey(pourDetector.liqourType))
                spawnPoints.Add(pourDetector.liqourType, pourDetector.gameObject.transform);
        }
    }

    public void RespawnBottle(LiquidType type, GameObject gameObject)
    {
        Instantiate(gameObject, spawnPoints[type]);
    }
}
