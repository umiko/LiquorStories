using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Cover : MonoBehaviour
{
    private GameObject shakerOBJ;
    private Shaker shaker;
    private Transform coverHolder;
    private MeshCollider nozzleCollider;
    private Rigidbody rigidbody;
    private Transform followTransform;

    [SerializeField]
    private bool isInHand = false;

    public bool IsInHand { get => isInHand; set => isInHand = value; }

    [SerializeField]
    private bool isAttached = false; //is the cover attached to the shaker

    public bool IsAttached
    {
        get { return isAttached; }
        set
        {
            Debug.Log("set isAttached");
            isAttached = value;
            // disable collision detection for the cover while attached to the shaker
            //rigidbody.detectCollisions = value ? false : true;

            if (value)
            {
                attachCover();
            }
            else
            {
                removeCover();
            }
        }
    }

    private void Awake()
    {
        shakerOBJ = GameObject.Find("Shaker");
        shaker = shakerOBJ.GetComponent<Shaker>();
        coverHolder = shakerOBJ.transform.Find("CoverHolder").transform;
        nozzleCollider = shakerOBJ.transform.Find("Nozzle").GetComponent<MeshCollider>();

        rigidbody = GetComponent<Rigidbody>();
    }

    //private void OnValidate()
    //{
    //    if (Application.isPlaying)
    //        IsAttached = isAttached;
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (!IsInHand && !isAttached)
    //    {
    //        foreach (ContactPoint contact in collision.contacts)
    //        {
    //            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);

    //            if (contact.otherCollider.name == "CoverHolder")
    //            {
    //                coverHolder = contact.otherCollider.transform;
    //                Vector3 position = coverHolder.position;

    //                transform.position = new Vector3(position.x, position.y, position.z);
    //                transform.rotation = coverHolder.rotation;
    //                gameObject.transform.SetParent(coverHolder.parent);

    //                isAttached = true;
    //                // notify shaker
    //                Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
    //                shaker.isCoverAttached = true;
    //            }
    //        }
    //    }
    //    else if (IsInHand && isAttached)
    //    {
    //        gameObject.transform.SetParent(null);
    //        isAttached = false;
    //        // notify shaker
    //        Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
    //        shaker.isCoverAttached = false;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        //print(other.name);
        if (!IsInHand && !IsAttached)
        {
            if (other.name == "CoverHolder")
            {
                //// disable collider on nozzle  to prevent collision with the cover
                //nozzleCollider.enabled = false;

                //coverHolder = other.transform;
                //Vector3 position = coverHolder.position;

                //transform.position = new Vector3(position.x, position.y + transform.lossyScale.y, position.z);
                //transform.rotation = coverHolder.rotation;
                //gameObject.transform.SetParent(coverHolder.parent);

                IsAttached = true;
                //// notify shaker
                //Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
                //shaker.isCoverAttached = true;
            }
        }
        else if (IsInHand && IsAttached)
        {
            //    gameObject.transform.SetParent(null);
            IsAttached = false;
            //// notify shaker
            //Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
            //shaker.isCoverAttached = false;

            //transform.position = new Vector3(transform.position.x, transform.position.y + (transform.lossyScale.y * 1.2f), transform.position.z);
            //nozzleCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (isAttached && !isInHand)
        {
            Vector3 position = coverHolder.position;
            transform.position = position + coverHolder.transform.rotation * new Vector3(0f, transform.lossyScale.y, 0f);
            transform.rotation = coverHolder.rotation;
        }
    }

    private void attachCover()
    {
        //Debug.Log("attachCover");
        nozzleCollider.enabled = false;
        Vector3 position = coverHolder.position;

        transform.position = new Vector3(position.x, position.y + transform.lossyScale.y, position.z);
        transform.rotation = coverHolder.rotation;

        shaker.isCoverAttached = true;
    }

    private void removeCover()
    {
        //Debug.Log("removeCover");
        shaker.isCoverAttached = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + (transform.lossyScale.y * 1.2f), transform.position.z);
        nozzleCollider.enabled = true;
    }
}