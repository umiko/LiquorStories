using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

[RequireComponent(typeof(SliceCooldown))]
public class Slicable : MonoBehaviour
{
    public Material mat;
    public int Pieces = 4;
    
    private GameObject[] slicedObjects;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!SliceRegistry.IsSlicer(other.transform))
        {
            return;
        }
        //This works only for the knife! If something is not cutting correctly and you arent using the knife prefab to cut, this is probably where the issue is at
        if (!(other.GetContact(0).thisCollider.gameObject.CompareTag("Sharp") || other.GetContact(0).otherCollider.gameObject.CompareTag("Sharp")))
        {
            return;
        }

        if (Pieces < 1)
        {
            return;
        }

        slicedObjects = this.gameObject.SliceInstantiate(other.transform.position, other.transform.forward, mat);
        if (slicedObjects != null)
        {
            AddRigidBodyAndExplosions();
            gameObject.SetActive(false);
        }
    }
    
    private void AddRigidBodyAndExplosions()
    {
        foreach(GameObject obj in slicedObjects)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            MeshCollider col = obj.AddComponent<MeshCollider>();
            col.convex = true;
            //SliceCooldown sc = obj.AddComponent<SliceCooldown>();
            SliceCooldown sli = obj.AddComponent<SliceCooldown>();
            sli.Pieces = Pieces - 1;
            sli.mat = mat;
            
            rb.AddExplosionForce(10, transform.position, 3);
        }
    }
    
}
