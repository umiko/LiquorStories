using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SliceCooldown : MonoBehaviour
{
    public Material mat;
    public int Pieces = 4;
    public Interactable interactable;
    public Throwable throwable;
    private Slicable mySlicable;
    
    public float Cooldown = .3f;
    private float timeAtInstantiation;
    private bool OnCooldown = true;
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        timeAtInstantiation = Time.time;
        //disable my slicable for cooldown
    }

    // Update is called once per frame
    private void Update()
    {
        if (OnCooldown)
        {
            OnCooldown = Time.time < (timeAtInstantiation + Cooldown);
            //early out
            return;
        }
        //add slicable after cooldown
        mySlicable = gameObject.AddComponent<Slicable>();
        mySlicable.Pieces = Pieces;
        mySlicable.mat = mat;
        gameObject.AddComponent<Interactable>();
        Throwable t = gameObject.AddComponent<Throwable>();
        t.attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.VelocityMovement |
                            Hand.AttachmentFlags.TurnOffGravity;
        //disable cooldown script
        this.enabled = false;
    }
}
