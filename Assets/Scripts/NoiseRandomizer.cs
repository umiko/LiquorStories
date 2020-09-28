using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]
public class NoiseRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public AudioListener audioListener;
 
    // Start is called before the first frame update
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        audioSource = GetComponent<AudioSource>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandom();
        }
    }
    void PlayRandom()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        
     
    }
}
