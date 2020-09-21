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

    private void Start()
    {
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
        print("Start");
        currentSteam = CreateStream();
        currentSteam.Begin();
        volumeControll = StartCoroutine(VolumeControll());
    }

    private void EndPour()
    {
        print("End");
        StopCoroutine(volumeControll);
        currentSteam.End();
        currentSteam = null;
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

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
}