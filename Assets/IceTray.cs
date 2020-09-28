using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTray : MonoBehaviour
{
    public GameObject iceCube;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "IceCube")
        {
            Debug.Log("Hier " + transform.name + " " + other.name + " ist raus");
            StartCoroutine("spawnIceCube");
            Destroy(other, 15f);
        }
    }

    private IEnumerator spawnIceCube()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(iceCube, transform.position, Quaternion.identity);
    }
}