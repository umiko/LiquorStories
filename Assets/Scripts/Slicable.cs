using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class Slicable : MonoBehaviour
{
    public Material mat;
    public int Pieces = 4;

    private GameObject[] slicedObjects;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("I AM COLLIDING");

        if (!SliceRegistry.IsSlicer(other.transform))
        {
            return;
        }
        Debug.Log("I AM BEING SLICED");
        if (!(other.GetContact(0).thisCollider.gameObject.CompareTag("Sharp") || other.GetContact(0).otherCollider.gameObject.CompareTag("Sharp")))
        {
            return;
        }
        Debug.Log("WITH A KNIFE");
        Transform otherTrans = other.transform;
        Debug.Log(otherTrans.position);
        Debug.Log(otherTrans.rotation.eulerAngles);
        Debug.Log(mat);

        
        slicedObjects = this.gameObject.SliceInstantiate(otherTrans.position,otherTrans.forward, mat);
        Debug.Log(slicedObjects);
        AddRigidBodyAndExplosions(slicedObjects);
        gameObject.SetActive(false);
    }
    
    private void AddRigidBodyAndExplosions(GameObject[] slicedObjects)
    {
        foreach(GameObject obj in slicedObjects)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            MeshCollider col = obj.AddComponent<MeshCollider>();
            col.convex = true;
            Slicable sli = obj.AddComponent<Slicable>();
            sli.Pieces = Pieces - 1;
            sli.mat = mat;
            rb.AddExplosionForce(10, transform.position, 3);
        }
    }
    
}
