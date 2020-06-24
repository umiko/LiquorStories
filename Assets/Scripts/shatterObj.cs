using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterObj : MonoBehaviour
{
    public GameObject breakVersion;
    public float breakForce;
    protected Rigidbody rigidbody;
    private int active = 0;
    public float radius;
    public AnimationCurve expoPower;
    public AudioClip clip;
    public float volume = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Shard")
        {
            Debug.Log(gameObject.name + " velocity: " + rigidbody.velocity.magnitude + " relative: " + collision.relativeVelocity.magnitude);
            //if (rigidbody.velocity.magnitude > breakForce && active == 0)
            if (collision.relativeVelocity.magnitude > breakForce && active == 0)
            {
                //Debug.Log(gameObject.name + ": " + rigidbody.velocity.magnitude + " " + expoPower.Evaluate(rigidbody.velocity.magnitude));
                //Debug.Log("ExPower: " + expoPower.Evaluate(rigidbody.velocity.magnitude));

                active++;

                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                GameObject instance = Instantiate(breakVersion, transform.position, transform.rotation);
                Vector3 explosionPos = transform.position;

                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        //rb.AddExplosionForce(power, explosionPos, radius, 0.0F, ForceMode.Impulse);
                        //Debug.Log("Power: " + expoPower.Evaluate(collision.relativeVelocity.magnitude));
                        rb.AddExplosionForce(expoPower.Evaluate(collision.relativeVelocity.magnitude), explosionPos, radius, 0.0F, ForceMode.Impulse);
                    }
                }

                //foreach (Transform child in instance.transform)
                //{
                //    //Debug.Log(child.gameObject.name);
                //    Rigidbody rb = child.GetComponent<Rigidbody>();
                //    if (rb != null)
                //    {
                //        rb.AddExplosionForce(expoPower.Evaluate(rigidbody.velocity.magnitude), explosionPos, radius, 0.0F, ForceMode.Impulse);
                //    }
                //}
                Destroy(gameObject);
            }
        }
    }
}