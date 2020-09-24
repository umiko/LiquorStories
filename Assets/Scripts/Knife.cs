using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void Start()
    {
        SliceRegistry.Register(this.transform);
    }

    void OnCollisionEnter(Collision other)
    {
        
        // Material mat = other.gameObject.GetComponent<MeshRenderer>().material;
        //
        // if (other.gameObject.CompareTag("Slicable"))
        // {
        //     Debug.Log("slice n dice");
        //
        //     GameObject[] slicedObjects = this.SliceObject(other.gameObject, transform.position,transform.rotation.eulerAngles, mat);
        //     other.gameObject.SetActive(false);
        //     AddRigidBodyAndExplosions(slicedObjects);
        // }
    }

    public GameObject[] SliceObject(GameObject obj, Vector3 worldPos, Vector3 worldDir, Material crossSectionMaterial)
    {
        return obj.SliceInstantiate(worldPos, worldDir, crossSectionMaterial);
    }
    
    private void AddRigidBodyAndExplosions(GameObject[] slicedObjects)
    {
        // foreach (GameObject obj in slicedObjects)
        // {
        //     Rigidbody rb = obj.AddComponent<Rigidbody>();
        //     rb.interpolation = RigidbodyInterpolation.Interpolate;
        //     MeshCollider col = obj.AddComponent<MeshCollider>();
        //     col.convex = true;
        //     
        //     rb.AddExplosionForce(100, transform.position, 20);
        // }
    }
}
