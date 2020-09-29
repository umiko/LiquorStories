using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int maxVolume = 1000;
    public int volume = 1000; //ml
    public bool isEmpty = false;
    public AnimationCurve pourAngleCurve; //Curve to adjust the pourThreshold
    private Coroutine volumeControll = null;

    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public float fillAmountClampFull;
    public float fillAmountClampEmpty;
    private Material liquidMaterialReference;
    private Renderer liquidRendererReference;

    private bool isPouring = false;

    public ParticleSystem particleSystem;
    public LiquidType liqourType;
    public List<ParticleCollisionEvent> collisionEvents;
    private Shaker shaker;
    private MeshCollider nozzleColider;

    public Color liquidColor;
    private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        liquidRendererReference = GetComponentInChildren<Wobble>().gameObject.GetComponent<Renderer>();
        liquidMaterialReference = liquidRendererReference.material;

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
        particleSystem.Play();
        volumeControll = StartCoroutine(VolumeControll());
    }

    private void EndPour()
    {
        StopCoroutine(volumeControll);
        particleSystem.Stop();
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    private IEnumerator VolumeControll()
    {
        while (gameObject.activeSelf && !isEmpty)
        {
            if (volume <= 0)
            {
                isEmpty = true;
                liquidRendererReference.enabled = false;
                EndPour();
            }
            else
            {
                volume--;
            }
            liquidMaterialReference.SetFloat(FillAmount, Mathf.Lerp(fillAmountClampEmpty, fillAmountClampFull, volume / 1000f));
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Refill Area") && volume < maxVolume)
        {
            this.volume++;
            isEmpty = false;
            liquidRendererReference.enabled = true;
            liquidMaterialReference.SetFloat(FillAmount, Mathf.Lerp(fillAmountClampEmpty, fillAmountClampFull, volume / 1000f));
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
            if (collisionEvents[i].colliderComponent.name == "Nozzle")
            {
                //Debug.Log("Attached OBJ " + other.name);
                shaker.addIngredient(new LiquidIngredient(liqourType), 1);
            }
            i++;
        }
    }
}