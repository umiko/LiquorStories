using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    private Transform coverHolder;

    [SerializeField]
    private bool isInHand = false;

    public bool IsInHand { get => isInHand; set => isInHand = value; }

    [SerializeField]
    private bool isAttached = false; //is the cover attached to the shaker

    private MeshCollider nozzleCollider;

    private void Awake()
    {
        nozzleCollider = GameObject.Find("Nozzle").GetComponent<MeshCollider>();
    }

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
        if (!isInHand && !isAttached)
        {
            if (other.name == "CoverHolder")
            {
                // disable collider on nozzle  to prevent collision with the cover
                nozzleCollider.enabled = false;

                coverHolder = other.transform;
                Vector3 position = coverHolder.position;

                transform.position = new Vector3(position.x, position.y + transform.lossyScale.y, position.z);
                transform.rotation = coverHolder.rotation;
                gameObject.transform.SetParent(coverHolder.parent);

                isAttached = true;
                // notify shaker
                Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
                shaker.isCoverAttached = true;
            }
        }
        else if (isInHand && isAttached)
        {
            gameObject.transform.SetParent(null);
            isAttached = false;
            // notify shaker
            Shaker shaker = coverHolder.GetComponentInParent<Shaker>();
            shaker.isCoverAttached = false;

            transform.position = new Vector3(transform.position.x, transform.position.y + (transform.lossyScale.y * 1.2f), transform.position.z);
            nozzleCollider.enabled = true;
        }
    }
}