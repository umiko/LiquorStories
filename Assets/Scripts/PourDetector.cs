using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int volume = 1000; //ml
    public bool isEmpty = false;
    public AnimationCurve pourAngleCurve; //Curve to adjust the pourThreshold
    private Coroutine volumeControll = null;

    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentSteam = null;

    public ParticleSystem particleSystem;
    public LiquidType liqourType;
    public List<ParticleCollisionEvent> collisionEvents;
    private Shaker shaker;
    private MeshCollider nozzleColider;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

        GameObject shakerOBJ = GameObject.Find("Shaker");
        shaker = shakerOBJ.GetComponent<Shaker>();
        nozzleColider = shakerOBJ.transform.Find("Nozzle").GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (!isEmpty)
        {
            pourThreshold = (int)pourAngleCurve.Evaluate((volume / 10));
            bool pourCheck = CalculatePourAngle() < pourThreshold;

            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;

                if (isPouring)
                {
                    StartPour();
                }
                else
                {
                    EndPour();
                }
            }
        }
    }

    private void StartPour()
    {
        //print("Start");
        //currentSteam = CreateStream();
        //currentSteam.Begin();
        particleSystem.Play();
        volumeControll = StartCoroutine(VolumeControll());
    }

    private void EndPour()
    {
        //print("End");
        StopCoroutine(volumeControll);
        particleSystem.Stop();
        //currentSteam.End();
        //currentSteam = null;
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    //private Stream CreateStream()
    //{
    //    GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
    //    return streamObject.GetComponent<Stream>();
    //}

    private IEnumerator VolumeControll()
    {
        while (gameObject.activeSelf && !isEmpty)
        {
            if (volume <= 0)
            {
                isEmpty = true;
                EndPour();
            }
            else
            {
                volume--;
            }

            yield return null;
        }
    }

    //public void UpdateLiqourType(LiquidType liqourType)
    //{
    //    this.liqourType = liqourType;

    //    var customData = particleSystem.customData;
    //    customData.enabled = true;
    //    customData.SetMode(ParticleSystemCustomData.Custom1, ParticleSystemCustomDataMode.Vector);
    //    customData.SetVectorComponentCount(ParticleSystemCustomData.Custom1, 1);
    //    customData.SetVector(ParticleSystemCustomData.Custom1, 0, (int)liqourType);
    //}

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            //Debug.Log("Particle Collision: " + other.name);
            //Debug.Log("Coll Event " + collisionEvents[i].colliderComponent.name);
            if (collisionEvents[i].colliderComponent.name == "Nozzle")
            {
                Debug.Log("Attached OBJ " + other.name);
                //Shaker shaker = other.GetComponentInParent<Shaker>();
                shaker.addIngredient(new LiquidIngredient(liqourType), 1);
            }
            i++;
        }
    }

    //private void OnParticleTrigger()
    //{
    //    List<ParticleSystem.Particle> enteredParticles = new List<ParticleSystem.Particle>();
    //    // get
    //    int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParticles);
    //    // iterate

    //    foreach (ParticleSystem.Particle particle in enteredParticles)
    //    {
    //        if (nozzleColider.bounds.Contains(particle.position))
    //        {
    //            Debug.Log("Particle collision mit nozzle");
    //        }
    //        else
    //        {
    //            Debug.Log("Particle collision mit was anderem");
    //        }
    //        // shaker.addIngredient(new LiquidIngredient(liqourType), 1);
    //    }

    //    ParticlePhysicsExtensions.GetTriggerParticles(particleSystem, ParticleSystemTriggerEventType.Enter, enteredParticles);
    //}
}