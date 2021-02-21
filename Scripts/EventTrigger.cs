using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent myTrigger;
    [SerializeField] public Camera playerCam;
    [SerializeField] bool deactivateTrigger = true;
    [SerializeField] float AngleToEvent;

    private Collider collider;
    private Transform childObj;
    private float angToPlayer;

    private bool collided = false;

    void Start() 
    {
        collider = GetComponent<SphereCollider>();

        childObj = transform.GetChild(0);

        if (childObj == null)
            return;
    }

    void Update() 
    {
        

        if (collided) 
        {
            Debug.Log(angToPlayer);
            LookForAngle();

            if (angToPlayer > AngleToEvent)
            {
                myTrigger.Invoke();
                if (deactivateTrigger)
                    this.enabled = false;
            }
        }
    }

    void LookForAngle() 
    {
        Vector3 direction = childObj.position - playerCam.transform.position;
        angToPlayer = Vector3.Angle(playerCam.transform.forward, direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collided = false;
        }
    }
}
