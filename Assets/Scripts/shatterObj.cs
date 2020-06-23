using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterObj : MonoBehaviour
{
    public GameObject breakVersion;
    public float breakForce;
    protected Rigidbody rigidbody;
    private int active = 0;
    public float power;
    public float radius;
    public AnimationCurve expoPower;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Debug.Log("BreakForce: " + breakForce + "\nPower: " + power + "\nRadius: " + radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rigidbody.velocity.magnitude > breakForce && active == 0)
        {
            Debug.Log(gameObject.name + ": " + rigidbody.velocity.magnitude + " " + expoPower.Evaluate(rigidbody.velocity.magnitude));
            //Debug.Log("ExPower: " + expoPower.Evaluate(rigidbody.velocity.magnitude));
            active++;
            GameObject instance = Instantiate(breakVersion, transform.position, transform.rotation);
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //rb.AddExplosionForce(power, explosionPos, radius, 0.0F, ForceMode.Impulse);
                    rb.AddExplosionForce(expoPower.Evaluate(rigidbody.velocity.magnitude), explosionPos, radius, 0.0F, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}