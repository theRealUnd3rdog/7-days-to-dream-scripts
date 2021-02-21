using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform doorHandle;
    private Rigidbody rb;

    void Start() 
    {
        
    }

    public void CloseDoor() 
    {
        doorHandle = this.transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(500f * -doorHandle.forward);
        rb.mass = 3000;
        Invoke("kinematicDelay", 1f);
    }

    void kinematicDelay() 
    {
        rb.isKinematic = true;
    }
}
