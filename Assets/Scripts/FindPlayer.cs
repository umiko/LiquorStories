using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class FindPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    void Start()
    {
        
        if (!Player.instance)
        {
            Instantiate(PlayerPrefab);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Player.instance.transform.position = transform.position;
        Player.instance.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
